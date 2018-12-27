using AutoMapper;
using OnlineShop.Common;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace OnlineShop.Web.API
{
    [RoutePrefix("app/email")]
    public class CustomerEmailController : ApiControllerBase
    {
        IEmailService _emailService;
        IEmailManagerService _emailManagerService;

        public CustomerEmailController(IErrorService errorService, IEmailService emailService, IEmailManagerService emailManagerService) : base(errorService)
        {
            this._emailService = emailService;
            this._emailManagerService = emailManagerService;
        }

        #region Customer_Emails_Manager
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAllEmails(HttpRequestMessage request, string keyword)
        {
            return CreateHttpReponse(request, () =>
            {
                var page = 0;
                var pageSize = Convert.ToInt32(ConfigHelper.GetValueByKey("pageSize"));
                var totalRow = 0;
                var listEmailModel = _emailService.GetAll(keyword, page, pageSize, out totalRow);
                var listEmailViewModel = Mapper.Map<IEnumerable<Email>, IEnumerable<EmailViewModel>>(listEmailModel);
                return request.CreateResponse(HttpStatusCode.OK, listEmailViewModel);
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteEmail(HttpRequestMessage request, int emailId)
        {
            if (ModelState.IsValid)
            {
                Func<HttpResponseMessage> func = delegate ()
                {
                    var emailModel = _emailService.DeleteEmail(emailId);
                    _emailService.Save();
                    var emailViewModel = Mapper.Map<Email, EmailViewModel>(emailModel);
                    return request.CreateResponse(HttpStatusCode.OK, emailViewModel);
                };
                return CreateHttpReponse(request, func);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("deleteMulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string ids)
        {
            if (ModelState.IsValid)
            {
                return CreateHttpReponse(request, () =>
                {
                    var listIds = new JavaScriptSerializer().Deserialize<List<int>>(ids);
                    foreach (var id in listIds)
                    {
                        _emailService.DeleteEmail(id);
                        _emailService.Save();
                    }
                    return request.CreateResponse(HttpStatusCode.OK, listIds.Count);
                });
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
        #endregion

        #region Email_Sent_Manager
        [HttpGet]
        [Route("total_email_count")]
        public HttpResponseMessage GetTotalEmail(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var emailList = _emailService.GetAll();
                var listEmailViewModel = Mapper.Map<IEnumerable<Email>, IEnumerable<EmailViewModel>>(emailList);
                EmailManagerViewModel emailManagerVm = new EmailManagerViewModel()
                {
                    EmailUser = ConfigHelper.GetValueByKey("FromEmailAddress"),
                    RecipientEmails = listEmailViewModel,
                };
                return request.CreateResponse(HttpStatusCode.OK, emailManagerVm);
            });
        }

        [HttpPost]
        [Route("send_mail")]
        public HttpResponseMessage SendMail(HttpRequestMessage request, EmailManagerViewModel emailManagerVm)
        {
            return CreateHttpReponse(request, () =>
            {
                var listEmailAddress = string.Empty;
                foreach (var email in emailManagerVm.RecipientEmails)
                {
                    listEmailAddress += $"{email.EmailAddress},";
                }
                listEmailAddress = listEmailAddress.Remove(listEmailAddress.Length - 1);
                try
                {
                    MailHelper.SendMail(listEmailAddress, emailManagerVm.MailTitle, emailManagerVm.MailContent);
                }
                catch (Exception ex)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }

                var EmailManagerModel = new EmailManager();
                EmailManagerModel.UpdateEmailManager(emailManagerVm);
                _emailManagerService.Add(EmailManagerModel);
                _emailManagerService.Save();
                return request.CreateResponse(HttpStatusCode.OK, emailManagerVm);
            });
        }

        [Route("list_email_sent")]
        [HttpGet]
        public HttpResponseMessage EmailSentManager(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = delegate ()
            {
                var listEmailManagerModel = _emailManagerService.GetAll();
                var listEmailManagerViewModel = Mapper.Map<IEnumerable<EmailManager>, IEnumerable<EmailManagerViewModel>>(listEmailManagerModel);
                return request.CreateResponse(HttpStatusCode.OK, listEmailManagerViewModel);
            };
            return CreateHttpReponse(request, func);
        }

        [Route("delete_email")]
        [HttpDelete]
        public HttpResponseMessage DeleteEmailSent(HttpRequestMessage request, int emailId)
        {
            if (ModelState.IsValid)
            {
                return CreateHttpReponse(request, () =>
                {
                    var emailModel = _emailManagerService.Delete(emailId);
                    _emailManagerService.Save();
                    var emailViewModel = Mapper.Map<EmailManager, EmailManagerViewModel>(emailModel);
                    return request.CreateResponse(HttpStatusCode.OK, emailViewModel);
                });
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("load_email/{emailId:int}")]
        [HttpGet]
        public HttpResponseMessage LoadEmailContent(HttpRequestMessage request, int emailId)
        {
            return CreateHttpReponse(request, delegate ()
            {
                var emailModel = _emailManagerService.GetSingleEmail(emailId);
                var emailViewModel = Mapper.Map<EmailManager, EmailManagerViewModel>(emailModel);
                emailViewModel.RecipientEmails = Mapper.Map<IEnumerable<Email>, IEnumerable<EmailViewModel>>(_emailService.GetAll());
                emailViewModel.RecipientCount = _emailService.GetAll().Count();
                return request.CreateResponse(HttpStatusCode.OK, emailViewModel);
            });
        }
        #endregion
    }
}
