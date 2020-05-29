using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Share;
using ViewModel.Contact;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;
using System.Data.Entity;
using WebTemplateDB.Interface;
using ViewModels.Verity;
using System.Net.Mail;
using System.Net;

namespace WebTemplateDB.Service
{
    public class ContactService : IContactService
    {
        WebTemplateEntities _db;
        protected IGenericRepository<Contact> _contact;

        public ContactService()
        {
            _db = new WebTemplateEntities();
            _contact = new GenericRepository<Contact>();
        }
        /// <summary>
        /// 取得連絡管理列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<ResWithPaginationViewModel> GetContactList(SearchModel searchModel, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel pageData = new ResWithPaginationViewModel();
            List<ContactViewModel> contactlist = new List<ContactViewModel>();

            var query = from a in _contact.GetAll().ToList()
                        select new ContactViewModel
                        {
                            ContactId = a.ContactId,
                            CompanyName = a.CompanyName,
                            Name = a.Name,
                            Sex = (int)a.Sex,
                            Email = a.Email,
                            ContactPhone = a.ContactPhone,
                            ContactEnum = (int)a.ContactEnum,
                            ContactStatus = (int)a.ContactStatus,
                            Budget = a.Budget,
                            OnlineDate = a.OnlineDate,
                            Demand = a.Demand,
                            Remark = a.Remark,
                            CreateTime = a.CreateTime,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };

            pageData.MinDateTime = query.OrderBy(x => x.CreateTime).FirstOrDefault().CreateTime;
            pageData.MaxDateTime = query.OrderByDescending(x => x.CreateTime).FirstOrDefault().CreateTime;

            if (searchModel.StartDateTime != null)
            {
                query = query.Where(x => x.CreateTime >= searchModel.StartDateTime);
            }
            if (searchModel.EndDateTime != null)
            {
                query = query.Where(x => x.CreateTime <= searchModel.EndDateTime);
            }
            if (!string.IsNullOrEmpty(searchModel.Query))
            {

                var QueryString = searchModel.Query.ToLower().Trim();

                switch (searchModel.SearchEnum)
                {
                    case 0:
                        query = query.Where(x => x.CompanyName.ToLower().Trim().Contains(QueryString));
                        break;
                    case 1:
                        query = query.Where(x => x.Name.ToLower().Trim().Contains(QueryString));
                        break;
                    case 2:
                        query = query.Where(x => x.ContactPhone.ToLower().Trim().Contains(QueryString));
                        break;
                    case 3:
                        if ("網站建置".Contains(QueryString))
                        {
                            int Enum = 0;
                            query = query.Where(x => x.ContactEnum == Enum);
                        }
                        if ("聊天機器人".Contains(QueryString))
                        {
                            int Enum = 1;
                            query = query.Where(x => x.ContactEnum == Enum);
                        }
                        if ("客製化抽獎".Contains(QueryString))
                        {
                            int Enum = 2;
                            query = query.Where(x => x.ContactEnum == Enum);
                        }
                        if ("其它".Contains(QueryString))
                        {
                            int Enum = 3;
                            query = query.Where(x => x.ContactEnum == Enum);
                        }
                        break;
                    default:
                        break;
                }
            }

            /**取得分頁資訊*/
            int _TotalCounts = query.Count();
            pageData.Pagination = new PaginationViewModel
            {
                PerPage = pagination.PerPage,
                CurrentPage = pagination.CurrentPage,
                TotalCounts = _TotalCounts
            };

            /**設置分頁資訊*/
            query = query
                .OrderByDescending(o => o.CreateTime)
                .Select((a, index) => new ContactViewModel
                {
                    ContactId = a.ContactId,
                    CompanyName = a.CompanyName,
                    Name = a.Name,
                    Sex = (int)a.Sex,
                    Email = a.Email,
                    ContactPhone = a.ContactPhone,
                    ContactEnum = (int)a.ContactEnum,
                    ContactStatus = (int)a.ContactStatus,
                    Budget = a.Budget,
                    OnlineDate = a.OnlineDate,
                    Demand = a.Demand,
                    Remark = a.Remark,
                    CreateTime = a.CreateTime,
                    UpdateTime = a.UpdateTime,
                    UpdateUser = a.UpdateUser,
                    Number = index + 1
                })
                .Skip(pagination.GetSkipLength())
                .Take(pagination.PerPage);

            if (query.Any())
            {
                var list = query.OrderByDescending(x => x.CreateTime).ToList();
                contactlist = list;
            }

            pageData.Data = contactlist;
            pageData.Success = true;
            return await Task.Run(() => pageData);
        }
        /// <summary>
        /// 取得編輯聯絡我們
        /// </summary>
        /// <param name="ContactId"></param>
        /// <returns></returns>
        public async Task<ContactViewModel> GetEditContact(int ContactId)
        {
            ContactViewModel contact = new ContactViewModel();

            var query = from a in _contact.FindBy(x => x.ContactId == ContactId)
                        select new ContactViewModel
                        {
                            ContactId = a.ContactId,
                            CompanyName = a.CompanyName,
                            Name = a.Name,
                            Sex = a.Sex,
                            Email = a.Email,
                            ContactPhone = a.ContactPhone,
                            ContactEnum = a.ContactEnum,
                            ContactStatus = a.ContactStatus,
                            Budget = a.Budget,
                            OnlineDate = a.OnlineDate,
                            Demand = a.Demand,
                            Remark = a.Remark,
                            CreateTime = a.CreateTime,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };

            if (query.Any())
            {
                var data = query.FirstOrDefault();
                contact = data;
            }

            return await Task.Run(() => contact);
        }
        /// <summary>
        /// 編輯聯絡我們
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CurrendUserid"></param>
        /// <returns></returns>
        public async Task<VerityResult> EditContactItem(ContactViewModel model, string CurrendUserid)
        {
            VerityResult result = new VerityResult();
            Contact _contactItem = new Contact();

            var query = _contact.FindBy(x => x.ContactId == model.ContactId);
            if (query.Any())
            {
                var data = query.FirstOrDefault();
                _contactItem = data;
                _contactItem.CompanyName = model.CompanyName;
                _contactItem.Name = model.Name;
                _contactItem.Sex = model.Sex;
                _contactItem.Email = model.Email;
                _contactItem.ContactPhone = model.ContactPhone;
                _contactItem.ContactEnum = model.ContactEnum;
                _contactItem.ContactStatus = model.ContactStatus;
                _contactItem.Budget = model.Budget;
                _contactItem.OnlineDate = model.OnlineDate;
                _contactItem.Demand = model.Demand;
                _contactItem.Remark = model.Remark;
                _contactItem.UpdateTime = DateTime.Now;
                _contactItem.UpdateUser = CurrendUserid;
            }

            try
            {
                _contact.Update(_contactItem);
                result.Success = true;
                result.Message = "編輯聯絡我們成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "編輯聯絡我們失敗";
            }
            return await Task.Run(() => result);
        }
        /// <summary>
        /// 刪除聯絡我們
        /// </summary>
        /// <param name="ContactId"></param>
        /// <returns></returns>
        public async Task<VerityResult> DeleteContact(int ContactId)
        {
            VerityResult result = new VerityResult();
            Contact _contactItem = new Contact();

            var query = _contact.FindBy(x => x.ContactId == ContactId);

            if (query.Any())
            {
                _contactItem = query.FirstOrDefault();
            }
            try
            {
                _contact.Delete(_contactItem);
                result.Success = true;
                result.Message = "編輯聯絡我們成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "編輯聯絡我們失敗";
            }
            return await Task.Run(() => result);
        }
        /// <summary>
        /// 新增聯絡我們 -- API Service
        /// </summary>
        /// <param name="model"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<VerityResult> CreateContact(ContactViewModel model, string url)
        {
            VerityResult result = new VerityResult();
            Contact contact = new Contact
            {
                CompanyName = model.CompanyName,
                Name = model.Name,
                Sex = model.Sex,
                Email = model.Email,
                ContactPhone = model.ContactPhone,
                ContactEnum = model.ContactEnum,
                ContactStatus = model.ContactStatus,
                Budget = model.Budget,
                OnlineDate = model.OnlineDate,
                Demand = model.Demand,
                Remark = model.Remark,
                CreateTime = DateTime.Now
            };
            try
            {
                _contact.Create(contact);
                switch (model.SendMode)
                {
                    case 0:
                        sendGmail(model);
                        break;
                    case 1:
                        SendMessage(model.Demand, url);
                        break;
                    default:
                        break;
                }
                result.Success = true;
                result.Message = "新增聯絡我們成功";
            }
            catch (Exception ex)
            {
                var aaa = ex.ToString();
                result.Success = false;
                result.Message = "新增聯絡我們失敗";
            }

            return await Task.Run(() => result);
        }
        /// <summary>
        /// Google發送Mail
        /// </summary>
        /// <param name="model"></param>
        public void sendGmail(ContactViewModel model)
        {
            MailMessage mail = new MailMessage();

            var SystemEmailName = _db.WebConfig.FirstOrDefault().SystemEmailName;
            var SystemEmail = _db.WebConfig.FirstOrDefault().SystemEmail;

            mail.IsBodyHtml = true;

            //前面是發信email後面是顯示的名稱
            mail.From = new MailAddress(model.Email, model.Name);

            //收信者email
            mail.To.Add(SystemEmail);

            //設定優先權
            mail.Priority = MailPriority.Normal;

            //標題
            mail.Subject = "聯絡我們";

            //內容
            var Sex = "";
            switch (model.Sex)
            {
                case 0:
                    Sex = "先生";
                    break;
                case 1:
                    Sex = "小姐";
                    break;
                default:
                    Sex = "先生/小姐";
                    break;
            };
            var Enum = "";
            switch (model.ContactEnum)
            {
                case 0:
                    Enum = "網站建置";
                    break;
                case 1:
                    Enum = "聊天機器人";
                    break;
                case 2:
                    Enum = "客製化抽獎";
                    break;
                case 3:
                    Enum = "其他";
                    break;
                default:
                    Enum = "未選擇";
                    break;
            }
            var Status = "";
            switch (model.ContactStatus)
            {
                case 0:
                    Status = "全新專案";
                    break;
                case 1:
                    Status = "改版增修";
                    break;
                default:
                    Status = "未選擇";
                    break;
            }
            mail.Body = "<p>您好：</p>" +
                "<p>		姓名                   ：" + model.Name + "  " + Sex + "</p>" +
                "<p>		Email                ：" + model.Email + "</p>" +
                "<p>		連絡電話          ：" + model.ContactPhone + "</p>" +
                "<p>		類型                   ：" + Enum + "</p>" +
                "<p>		狀態                   ：" + Status + "</p>" +
                "<p>        預算					：" + model.Budget + "</p>" +
                "<p>		預計上線區間 ：" + model.OnlineDate + "</p>" +
                "<p>		需求					：" + model.Demand + "</p>";


            //內容使用html
            mail.IsBodyHtml = true;

            //設定gmail的smtp (這是google的)
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

            //您在gmail的帳號密碼
            MySmtp.Credentials = new NetworkCredential("xinyu@sp88.com.tw", "zerock851024");

            //開啟ssl
            MySmtp.EnableSsl = true;

            //發送郵件
            MySmtp.Send(mail);

            //放掉宣告出來的MySmtp
            MySmtp = null;

            //放掉宣告出來的mail
            mail.Dispose();
        }

        /// <summary>
        /// 簡王發送簡訊訊
        /// </summary>
        private string SendMessage(string smbody, string responseUrl)
        {
            WebClient client = new WebClient();
            var url = string.Format("https://api.twsms.com/json/sms_send.php?username={0}&password={1}&mobile={2}&message={3}",
               "xinyu0124",
               "zerock851024",
               "0916723173",
               smbody
               );
            var result = client.DownloadString(url);
            client.Dispose();
            var aa = result;
            return result;
        }
    }
}
