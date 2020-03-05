import { Vue, Component, Watch, Emit, Prop } from 'vue-property-decorator'
import { FontHomeViewModel, SearchModel } from './model'
import service from './service'
import moment = require('moment')
import { UrlPathEnum } from '../Share/Enums'
import { dateToDateTimeString, dateToDateString } from "../Share/FilterFunction";

@Component({
    template: '#FontHomeManagement',
    filters: {
        dateToDateTimeString,
    }
})


export default class FontHomeManagement extends Vue {
    ListItem: FontHomeViewModel[] = []

    StartDateTime: string = moment().startOf('day').format('YYYY-MM-DD');
    EndDateTime: string = moment().endOf('day').format('YYYY-MM-DD');
    OnlineDateTime: string = moment().format('YYYY-MM-DD');
    Query: string = '';

    MaxDate: string = '';
    MinDate: string = '';
    OnlineMaxDate: string = '';
    OnlineMinDate: string = '';

    PerPage: number = 10;
    CurrentPage: number = 1;
    TotalPage: number = 1;
    TotalCounts: number = 1;

    searchmodel: SearchModel | null = null

    Pagination: PaginationViewModel = { PerPage: 10, CurrentPage: 1, TotalCounts: 0, TotalPage: 1 };

    Selectd: number = 0;
    Options: object[] = [{
        value: 0,
        text: '圖片名稱'
    }, {
        value: 1,
        text: '連結網址'
    }, {
        value: 2,
        text: '備註'
    }];

    created() {
        const _this = this;
        _this.SetDefaultSearchModel();
    }

    SetDefaultSearchModel() {
        const _this = this;
        _this.searchmodel = {
            Query: "",
            StartDateTime: null,
            EndDateTime: null,
            SearchEnum: 0,
            OnlineDateTime: null
        }
        _this.GetFontHomeList(_this.searchmodel);
    }

    GetFontHomeList(searchmodel: SearchModel) {
        const _this = this;
        const sendPagination: SendPaginationModel = {
            PerPage: _this.Pagination.PerPage,
            CurrentPage: _this.Pagination.CurrentPage
        }
        _this.DefaultFontHomeListItem(searchmodel, sendPagination);
    }

    DefaultFontHomeListItem(searchmodel, sendPagination) {
        const _this = this;

        service.GetFontHomeList(searchmodel, sendPagination).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.ListItem = res.Data;
                _this.Pagination = res.Pagination;

                _this.LimitDate(res);
                _this.CheckOnlineDate();

            }
        }).catch(err => {
            console.log(err);
        })
    }

    LimitDate(Data) {
        const _this = this;
        _this.StartDateTime = moment(Data.MinDateTime).startOf('day').format("YYYY-MM-DD");
        _this.EndDateTime = moment(Data.MaxDateTime).endOf('day').format("YYYY-MM-DD");
        _this.MinDate = _this.StartDateTime;
        _this.MaxDate = _this.EndDateTime;
    }

    SetSearchDate() {
        const _this = this;
        const sendPagination: SendPaginationModel = {
            PerPage: _this.Pagination.PerPage,
            CurrentPage: _this.Pagination.CurrentPage
        }

        _this.searchmodel = {
            SearchEnum: _this.Selectd,
            StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
            EndDateTime: moment(_this.EndDateTime).endOf('day').toDate(),
            Query: _this.Query,
            OnlineDateTime: moment(_this.OnlineDateTime).toDate()
        }

        console.log(_this.searchmodel);

        _this.GetFontHomeListItem(_this.searchmodel, sendPagination);
    }

    GetFontHomeListItem(searchmodel, sendPagination) {
        const _this = this;

        service.GetFontHomeList(searchmodel, sendPagination).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.ListItem = res.Data;
                _this.Pagination = res.Pagination;

                _this.CheckOnlineDate();
            }
        }).catch(err => {
            console.log(err);
        })
    }

    CheckOnlineDate() {
        const _this = this;
        if (_this.ListItem) {
            const length = _this.ListItem.length;
            for (var i = 0; i < length; i++) {
                const nowDate = moment().toDate();
                if (moment(_this.ListItem[i].StartDateTime).toDate() <= nowDate && moment(_this.ListItem[i].EndDateTime).toDate() >= nowDate && _this.ListItem[i].Status == true) {
                    _this.ListItem[i].CheckOnlineDate = true;
                } else {
                    _this.ListItem[i].CheckOnlineDate = false;
                }
            }
        }
    }

    @Watch('ListItem')
    GetImageUrl() {
        const _this = this;
        if (_this.ListItem) {
            const BasePath = window.BasePath; // _Layout.cshtml
            var length = _this.ListItem.length;
            for (var i = 0; i < length; i++) {
                _this.ListItem[i].ImgUrl = BasePath + UrlPathEnum.FontHomePhoto + '?filename=' + _this.ListItem[i].ImageName;
            }
        }
    }

    @Watch('ListItem')
    OnRideRecordListChange() {
        const _this = this;
        const _TotalCounts = _this.Pagination ? _this.Pagination.TotalCounts : 0;
        const _TotalPage = _this.Pagination ? Math.ceil(_this.Pagination.TotalCounts / _this.Pagination.PerPage) : 0;
        _this.TotalCounts = _TotalCounts;
        _this.TotalPage = _TotalPage === 0 ? 1 : _TotalPage;
    }

    @Watch('Pagination')
    OnPaginationChange() {
        const _this = this;
        if (_this.Pagination) {
            _this.PerPage = _this.Pagination.PerPage;
            _this.CurrentPage = _this.Pagination.CurrentPage;
            _this.TotalPage = _this.Pagination.TotalPage;
            _this.TotalCounts = _this.Pagination.TotalCounts;
        }
    }


    @Watch('PerPage')
    OnPerPageChange() {
        this.SetSendPagination();
    }
    @Watch('CurrentPage')
    OnCurrentPageChange() {
        this.SetSendPagination();
    }

    SetSendPagination() {
        const _this = this;
        _this.Pagination.PerPage = _this.PerPage;
        _this.Pagination.CurrentPage = _this.CurrentPage;
        if (!_this.searchmodel) { return; }
        else {
            if (_this.searchmodel.Query) {
                _this.searchmodel.Query = _this.Query;
            }
            _this.searchmodel.StartDateTime = moment(_this.StartDateTime).startOf('day').toDate();
            _this.searchmodel.EndDateTime = moment(_this.EndDateTime).endOf('day').toDate();
            _this.GetFontHomeList(_this.searchmodel);
        }
    }

    GetEditFontHome(FontHomeId: number) {
        var baseurl = window.BasePath;
        console.log(baseurl);
        var url = baseurl + 'FontHome/GetEditFontHome?FontHomeId=' + FontHomeId;
        window.location.href = url;
    }

    DeleteFontHome(FontHomeId: number) {
        const _this = this;
        service.DeleteFontHome(FontHomeId).then(res => {
            if (!res.Success) {
                console.log(res);
                _this.$bvToast.toast('刪除前台首頁圖片失敗', {
                    title: '前台首頁圖片',
                    variant: 'warning',
                })
            }
            if (res.Success) {
                _this.$bvToast.toast('刪除前台首頁圖片成功', {
                    title: '前台首頁圖片',
                    variant:'success',
                })
                _this.SetSearchDate();
            }
        }).catch(err => {
            console.log(err);
            _this.$bvToast.toast('與伺服器連接發生錯誤', {
                title: '前台首頁圖片',
                variant: 'danger',
            })
        })
    }
}