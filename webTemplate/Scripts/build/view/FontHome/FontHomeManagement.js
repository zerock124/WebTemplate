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
define(["require", "exports", "vue-property-decorator", "./service", "moment", "../Share/Enums", "../Share/FilterFunction"], function (require, exports, vue_property_decorator_1, service_1, moment, Enums_1, FilterFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    var FontHomeManagement = (function (_super) {
        __extends(FontHomeManagement, _super);
        function FontHomeManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.ListItem = [];
            _this_1.StartDateTime = moment().startOf('day').format('YYYY-MM-DD');
            _this_1.EndDateTime = moment().endOf('day').format('YYYY-MM-DD');
            _this_1.OnlineDateTime = moment().format('YYYY-MM-DD');
            _this_1.Query = '';
            _this_1.MaxDate = '';
            _this_1.MinDate = '';
            _this_1.OnlineMaxDate = '';
            _this_1.OnlineMinDate = '';
            _this_1.PerPage = 10;
            _this_1.CurrentPage = 1;
            _this_1.TotalPage = 1;
            _this_1.TotalCounts = 1;
            _this_1.searchmodel = null;
            _this_1.Pagination = { PerPage: 10, CurrentPage: 1, TotalCounts: 0, TotalPage: 1 };
            _this_1.Selectd = 0;
            _this_1.Options = [{
                    value: 0,
                    text: '圖片名稱'
                }, {
                    value: 1,
                    text: '連結網址'
                }, {
                    value: 2,
                    text: '備註'
                }];
            return _this_1;
        }
        FontHomeManagement.prototype.created = function () {
            var _this = this;
            _this.SetDefaultSearchModel();
        };
        FontHomeManagement.prototype.SetDefaultSearchModel = function () {
            var _this = this;
            _this.searchmodel = {
                Query: "",
                StartDateTime: null,
                EndDateTime: null,
                SearchEnum: 0,
                OnlineDateTime: null
            };
            _this.GetFontHomeList(_this.searchmodel);
        };
        FontHomeManagement.prototype.GetFontHomeList = function (searchmodel) {
            var _this = this;
            var sendPagination = {
                PerPage: _this.Pagination.PerPage,
                CurrentPage: _this.Pagination.CurrentPage
            };
            _this.DefaultFontHomeListItem(searchmodel, sendPagination);
        };
        FontHomeManagement.prototype.DefaultFontHomeListItem = function (searchmodel, sendPagination) {
            var _this = this;
            service_1.default.GetFontHomeList(searchmodel, sendPagination).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.ListItem = res.Data;
                    _this.Pagination = res.Pagination;
                    _this.LimitDate(res);
                    _this.CheckOnlineDate();
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        FontHomeManagement.prototype.LimitDate = function (Data) {
            var _this = this;
            _this.StartDateTime = moment(Data.MinDateTime).startOf('day').format("YYYY-MM-DD");
            _this.EndDateTime = moment(Data.MaxDateTime).endOf('day').format("YYYY-MM-DD");
            _this.MinDate = _this.StartDateTime;
            _this.MaxDate = _this.EndDateTime;
        };
        FontHomeManagement.prototype.SetSearchDate = function () {
            var _this = this;
            var sendPagination = {
                PerPage: _this.Pagination.PerPage,
                CurrentPage: _this.Pagination.CurrentPage
            };
            _this.searchmodel = {
                SearchEnum: _this.Selectd,
                StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
                EndDateTime: moment(_this.EndDateTime).endOf('day').toDate(),
                Query: _this.Query,
                OnlineDateTime: moment(_this.OnlineDateTime).toDate()
            };
            console.log(_this.searchmodel);
            _this.GetFontHomeListItem(_this.searchmodel, sendPagination);
        };
        FontHomeManagement.prototype.GetFontHomeListItem = function (searchmodel, sendPagination) {
            var _this = this;
            service_1.default.GetFontHomeList(searchmodel, sendPagination).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.ListItem = res.Data;
                    _this.Pagination = res.Pagination;
                    _this.CheckOnlineDate();
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        FontHomeManagement.prototype.CheckOnlineDate = function () {
            var _this = this;
            if (_this.ListItem) {
                var length_1 = _this.ListItem.length;
                for (var i = 0; i < length_1; i++) {
                    var nowDate = moment().toDate();
                    if (moment(_this.ListItem[i].StartDateTime).toDate() <= nowDate && moment(_this.ListItem[i].EndDateTime).toDate() >= nowDate && _this.ListItem[i].Status == true) {
                        _this.ListItem[i].CheckOnlineDate = true;
                    }
                    else {
                        _this.ListItem[i].CheckOnlineDate = false;
                    }
                }
            }
        };
        FontHomeManagement.prototype.GetImageUrl = function () {
            var _this = this;
            if (_this.ListItem) {
                var BasePath = window.BasePath;
                var length = _this.ListItem.length;
                for (var i = 0; i < length; i++) {
                    _this.ListItem[i].ImgUrl = BasePath + Enums_1.UrlPathEnum.FontHomePhoto + '?filename=' + _this.ListItem[i].ImageName;
                }
            }
        };
        FontHomeManagement.prototype.OnRideRecordListChange = function () {
            var _this = this;
            var _TotalCounts = _this.Pagination ? _this.Pagination.TotalCounts : 0;
            var _TotalPage = _this.Pagination ? Math.ceil(_this.Pagination.TotalCounts / _this.Pagination.PerPage) : 0;
            _this.TotalCounts = _TotalCounts;
            _this.TotalPage = _TotalPage === 0 ? 1 : _TotalPage;
        };
        FontHomeManagement.prototype.OnPaginationChange = function () {
            var _this = this;
            if (_this.Pagination) {
                _this.PerPage = _this.Pagination.PerPage;
                _this.CurrentPage = _this.Pagination.CurrentPage;
                _this.TotalPage = _this.Pagination.TotalPage;
                _this.TotalCounts = _this.Pagination.TotalCounts;
            }
        };
        FontHomeManagement.prototype.OnPerPageChange = function () {
            this.SetSendPagination();
        };
        FontHomeManagement.prototype.OnCurrentPageChange = function () {
            this.SetSendPagination();
        };
        FontHomeManagement.prototype.SetSendPagination = function () {
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
                _this.GetFontHomeList(_this.searchmodel);
            }
        };
        FontHomeManagement.prototype.GetEditFontHome = function (FontHomeId) {
            var baseurl = window.BasePath;
            console.log(baseurl);
            var url = baseurl + 'FontHome/GetEditFontHome?FontHomeId=' + FontHomeId;
            window.location.href = url;
        };
        FontHomeManagement.prototype.DeleteFontHome = function (FontHomeId) {
            var _this = this;
            service_1.default.DeleteFontHome(FontHomeId).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                    _this.$bvToast.toast('刪除前台首頁圖片失敗', {
                        title: '前台首頁圖片',
                        variant: 'warning',
                    });
                }
                if (res.Success) {
                    _this.$bvToast.toast('刪除前台首頁圖片成功', {
                        title: '前台首頁圖片',
                        variant: 'success',
                    });
                    _this.SetSearchDate();
                }
            }).catch(function (err) {
                console.log(err);
                _this.$bvToast.toast('與伺服器連接發生錯誤', {
                    title: '前台首頁圖片',
                    variant: 'danger',
                });
            });
        };
        __decorate([
            vue_property_decorator_1.Watch('ListItem')
        ], FontHomeManagement.prototype, "GetImageUrl", null);
        __decorate([
            vue_property_decorator_1.Watch('ListItem')
        ], FontHomeManagement.prototype, "OnRideRecordListChange", null);
        __decorate([
            vue_property_decorator_1.Watch('Pagination')
        ], FontHomeManagement.prototype, "OnPaginationChange", null);
        __decorate([
            vue_property_decorator_1.Watch('PerPage')
        ], FontHomeManagement.prototype, "OnPerPageChange", null);
        __decorate([
            vue_property_decorator_1.Watch('CurrentPage')
        ], FontHomeManagement.prototype, "OnCurrentPageChange", null);
        FontHomeManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#FontHomeManagement',
                filters: {
                    dateToDateTimeString: FilterFunction_1.dateToDateTimeString,
                }
            })
        ], FontHomeManagement);
        return FontHomeManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = FontHomeManagement;
});
//# sourceMappingURL=FontHomeManagement.js.map