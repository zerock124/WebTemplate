var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "vue-property-decorator", "./service", "moment", "../Share/FilterFunction"], function (require, exports, vue_property_decorator_1, service_1, moment, FilterFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    var ContactManagement = (function (_super) {
        __extends(ContactManagement, _super);
        function ContactManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.ListItem = [];
            _this_1.StartDateTime = moment().startOf('day').format('YYYY-MM-DD');
            _this_1.EndDateTime = moment().endOf('day').format('YYYY-MM-DD');
            _this_1.OnlineDateTime = moment().format('YYYY-MM-DD');
            _this_1.Query = '';
            _this_1.MaxDate = '';
            _this_1.MinDate = '';
            _this_1.PerPage = 10;
            _this_1.CurrentPage = 1;
            _this_1.TotalPage = 1;
            _this_1.TotalCounts = 0;
            _this_1.searchmodel = null;
            _this_1.Pagination = { PerPage: 10, CurrentPage: 1, TotalCounts: 0, TotalPage: 1 };
            _this_1.Selectd = 0;
            _this_1.Options = [{
                    value: 0,
                    text: '公司名稱'
                }, {
                    value: 1,
                    text: '聯絡人姓名'
                }, {
                    value: 2,
                    text: '聯絡人電話'
                }, {
                    value: 3,
                    text: '類型'
                }];
            return _this_1;
        }
        ContactManagement.prototype.created = function () {
            var _this = this;
            _this.SetDefaultSearchModel();
        };
        ContactManagement.prototype.SetDefaultSearchModel = function () {
            var _this = this;
            _this.searchmodel = {
                Query: "",
                StartDateTime: null,
                EndDateTime: null,
                SearchEnum: 0,
            };
            _this.GetContactList(_this.searchmodel);
        };
        ContactManagement.prototype.GetContactList = function (searchmodel) {
            var _this = this;
            var sendPagination = {
                PerPage: _this.Pagination.PerPage,
                CurrentPage: _this.Pagination.CurrentPage
            };
            _this.DefaultContactListItem(searchmodel, sendPagination);
        };
        ContactManagement.prototype.DefaultContactListItem = function (searchmodel, sendPagination) {
            var _this = this;
            service_1.default.GetContactList(searchmodel, sendPagination).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.ListItem = res.Data;
                    _this.Pagination = res.Pagination;
                    _this.LimitDate(res);
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        ContactManagement.prototype.LimitDate = function (Data) {
            var _this = this;
            console.log(Data);
            _this.StartDateTime = moment(Data.MinDateTime).startOf('day').format("YYYY-MM-DD");
            _this.EndDateTime = moment(Data.MaxDateTime).endOf('day').format("YYYY-MM-DD");
            _this.MinDate = _this.StartDateTime;
            _this.MaxDate = _this.EndDateTime;
        };
        ContactManagement.prototype.SetSearchDate = function () {
            var _this = this;
            _this.Pagination.PerPage = 10;
            _this.Pagination.CurrentPage = 1;
            var sendPagination = {
                PerPage: _this.Pagination.PerPage,
                CurrentPage: _this.Pagination.CurrentPage
            };
            _this.searchmodel = {
                SearchEnum: _this.Selectd,
                StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
                EndDateTime: moment(_this.EndDateTime).endOf('day').toDate(),
                Query: _this.Query,
            };
            console.log(_this.searchmodel);
            _this.GetContactListItem(_this.searchmodel, sendPagination);
        };
        ContactManagement.prototype.GetContactListItem = function (searchmodel, sendPagination) {
            var _this = this;
            service_1.default.GetContactList(searchmodel, sendPagination).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.ListItem = res.Data;
                    _this.Pagination = res.Pagination;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        ContactManagement.prototype.OnRideRecordListChange = function () {
            var _this = this;
            var _TotalCounts = _this.Pagination ? _this.Pagination.TotalCounts : 0;
            var _TotalPage = _this.Pagination ? Math.ceil(_this.Pagination.TotalCounts / _this.Pagination.PerPage) : 0;
            _this.TotalCounts = _TotalCounts;
            _this.TotalPage = _TotalPage === 0 ? 1 : _TotalPage;
        };
        ContactManagement.prototype.OnPaginationChange = function () {
            var _this = this;
            if (_this.Pagination) {
                _this.PerPage = _this.Pagination.PerPage;
                _this.CurrentPage = _this.Pagination.CurrentPage;
                _this.TotalPage = _this.Pagination.TotalPage;
                _this.TotalCounts = _this.Pagination.TotalCounts;
            }
        };
        ContactManagement.prototype.SetSendPagination = function () {
            var _this = this;
            _this.Pagination.PerPage = _this.PerPage;
            _this.Pagination.CurrentPage = _this.CurrentPage;
            if (!_this.searchmodel) {
                return;
            }
            else {
                if (_this.searchmodel.Query) {
                    _this.searchmodel.Query = _this.Query;
                }
                _this.searchmodel.StartDateTime = moment(_this.StartDateTime).startOf('day').toDate();
                _this.searchmodel.EndDateTime = moment(_this.EndDateTime).endOf('day').toDate();
                _this.GetContactList(_this.searchmodel);
            }
        };
        ContactManagement.prototype.GetEditContact = function (ContactId) {
            var baseurl = window.BasePath;
            console.log(baseurl);
            var url = baseurl + 'Contact/Edit?ContactId=' + ContactId;
            window.location.href = url;
        };
        ContactManagement.prototype.DeleteContact = function (ContactId) {
            var _this = this;
            service_1.default.DeleteContact(ContactId).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                    _this.$bvToast.toast('刪除聯絡紀錄失敗', {
                        title: '聯絡我們管理',
                        variant: 'warning',
                    });
                }
                if (res.Success) {
                    _this.$bvToast.toast('刪除聯絡紀錄成功', {
                        title: '聯絡我們管理',
                        variant: 'success',
                    });
                    _this.SetSearchDate();
                }
            }).catch(function (err) {
                console.log(err);
                _this.$bvToast.toast('與伺服器連接發生錯誤', {
                    title: '聯絡我們管理',
                    variant: 'danger',
                });
            });
        };
        __decorate([
            vue_property_decorator_1.Watch('ListItem')
        ], ContactManagement.prototype, "OnRideRecordListChange", null);
        __decorate([
            vue_property_decorator_1.Watch('Pagination')
        ], ContactManagement.prototype, "OnPaginationChange", null);
        ContactManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#ContactManagement',
                filters: {
                    dateToDateTimeString: FilterFunction_1.dateToDateTimeString,
                    GetSexEnum: FilterFunction_1.GetSexEnum,
                    GetContactEnum: FilterFunction_1.GetContactEnum,
                    GetContactStatus: FilterFunction_1.GetContactStatus
                }
            })
        ], ContactManagement);
        return ContactManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = ContactManagement;
});
//# sourceMappingURL=ContactManagement.js.map