﻿using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views;
using face_api_wpf_support.Views.business_face_library;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace face_api_wpf_support.ViewModels.business_face_library
{
    public class BusinessFaceLibraryViewModel: ObservableObject
    {
        private bool _next_page_checked;
        public bool next_page_checked
        {
            get { return _next_page_checked; }
            set
            {
                _next_page_checked = value;
                RaisePropertyChangedEvent("next_page_checked");
            }
        }

        private Page _next_page;
        public Page next_page
        {
            get
            {
                return _next_page;
            }

            set
            {
                _next_page = value;
            }
        }

        private bool _dialog_open;
        public bool Dialog_open
        {
            get
            {
                return _dialog_open;
            }

            set
            {
                _dialog_open = value;
                RaisePropertyChangedEvent("Dialog_open");
            }
        }

        RelayCommand _go_back_command;
        public RelayCommand go_back_command
        {
            get
            {
                if (_go_back_command == null)
                    _go_back_command = new RelayCommand(new Action<object>(go_home));
                return _go_back_command;
            }
            set
            {
                _go_back_command = value;
            }

        }

        private void go_home(object obj)
        {
            ListViewPage list_view_page = new ListViewPage();
            next_page = list_view_page;
            next_page_checked = true;

        }

        RelayCommand _add_face_command;
        public RelayCommand add_face_command
        {
            get
            {
                if (_add_face_command == null)
                    _add_face_command = new RelayCommand(new Action<object>(add_face));
                return _add_face_command;
            }
            set
            {
                _add_face_command = value;
            }

        }

        private void add_face(object obj)
        {
            UploadFacePhotoPage upload_face_photo_page = new UploadFacePhotoPage();
            next_page = upload_face_photo_page;
            next_page_checked = true;

        }


        RelayCommand _delete_face_command;
        public RelayCommand delete_face_command
        {
            get
            {
                if (_delete_face_command == null)
                    _delete_face_command = new RelayCommand(new Action<object>(delete_face));
                return _delete_face_command;
            }
            set
            {
                _delete_face_command = value;
            }

        }

        private async void delete_face(object obj)
        {
            Console.WriteLine((string)obj);
            string face_doc_id = obj.ToString();
            bool face_api_error = false;
            FaceDocs face_doc = null;
            FaceDocRepository face_doc_repository = null;

            Task get_face_doc_task = Task.Factory.StartNew(
                () =>
                {
                    using (var context = new DemoContext())
                    {
                        face_doc = (from fd in context.FaceDocs
                                        where fd.FaceDocId.Equals(face_doc_id)
                                        select fd).FirstOrDefault();

                        face_doc_repository = (from fdr in context.FaceDocRepository
                                                   where fdr.FaceDocId.Equals(face_doc_id)
                                                   select fdr).FirstOrDefault();
                    }
                });

            get_face_doc_task.Wait();

            var faceServiceClient = new FaceServiceClient();

            try
            {
                Guid persistedFaceId = new Guid(face_doc_id);
                await faceServiceClient.DeleteFaceFromFaceListAsync(face_doc_repository.FaceRepositoryId, persistedFaceId);
            }
            catch (FaceAPIException ex)
            {
                face_api_error = true;
                Console.WriteLine(ex.ErrorMessage);
            }


            if (face_api_error)
            {
                Dialog_open = true;
            }
            else
            {

                Task remove_face_doc_task = Task.Factory.StartNew(
                () =>
                {
                    using (var context = new DemoContext())
                    {
                        if (face_doc != null && face_doc_repository != null)
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    context.FaceDocs.Remove(face_doc);
                                    context.FaceDocRepository.Remove(face_doc_repository);
                                    context.SaveChanges();
                                    dbContextTransaction.Commit();

                                }
                                catch (Exception e)
                                {
                                    dbContextTransaction.Rollback();
                                }

                            }

                        }
                    }

                });
                remove_face_doc_task.Wait();
            }


            BusinessFaceLibraryPage business_face_library_page = new BusinessFaceLibraryPage();
            business_face_library_page.load_item();
            next_page = business_face_library_page;
            next_page_checked = true;

        }

        private List<FaceDocItem> _face_doc_list;
        public List<FaceDocItem> face_doc_list
        {
            get
            {
                return _face_doc_list;
            }

            set
            {
                _face_doc_list = value;
            }
        }

        public BusinessFaceLibraryViewModel()
        {

        }
        public void load_item()
        {
            string user_name = Thread.CurrentPrincipal.Identity.Name;
            Console.WriteLine(user_name);

            List<FaceDocItem> result = new List<FaceDocItem>();

            Task<List<FaceDocItem>> get_repository_task = Task<List<FaceDocItem>>.Factory.StartNew(
                () =>
                {

                    using (var context = new DemoContext())
                    {
                        var face_doc_list = from face_docs in context.FaceDocs
                                            join fdr in context.FaceDocRepository on face_docs.FaceDocId equals fdr.FaceDocId
                                            join fr in context.FaceRepository on fdr.FaceRepositoryId equals fr.FaceRepositoryId
                                            join frbc in context.FaceRepositoryBusinessClient on fr.Id equals frbc.FaceReposityId
                                            join ubc in context.UserBusinessClient on frbc.BusinessClientId equals ubc.BusinessClientId
                                            join user in context.User on ubc.UserId equals user.Id
                                            where user.UserName.Equals(user_name)
                                            select face_docs;

                        foreach (var face_doc in face_doc_list)
                        {
                            result.Add(new FaceDocItem(face_doc.FaceDocId, face_doc.UserData));
                        }
                    }

                    return result;
                });

            get_repository_task.Wait();

            face_doc_list = get_repository_task.Result;
        }
    }
}
