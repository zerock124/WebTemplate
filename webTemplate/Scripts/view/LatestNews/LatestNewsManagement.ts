import { Vue, Component, Watch, Emit, Prop } from 'vue-property-decorator'
import { LatestNewsViewModel, SearchModel } from './model'
import service from './service'
import moment = require('moment')
import { UrlPathEnum } from '../Share/Enums'
import { dateToDateTimeString, GetLatestNewsEnum } from "../Share/FilterFunction";

@Component({
    template: '#LatestNewsManagement',
    filters: {
        dateToDateTimeString,
        GetLatestNewsEnum
    }
})


export default class LatestNewsManagement extends Vue {
    ListItem: LatestNewsViewModel[] = []

    StartDateTime: string = moment().startOf('day').format('YYYY-MM-DD');
    EndDateTime: string = moment().endOf('day').format('YYYY-MM-DD');
    StartOnlineDateTime: string = moment().format('YYYY-MM-DD');
    EndOnlineDateTime: string = moment().format('YYYY-MM-DD');
    Query: string = '';

    MaxDate: string = '';
    MinDate: string = '';
    OnlineMaxDate: string = '';
    OnlineMinDate: string = '';

    PerPage: number = 10;
    CurrentPage: number = 1;
    TotalPage: number = 1;
    TotalCounts: number = 0;

    searchmodel: SearchModel | null = null

    Pagination: PaginationViewModel = { PerPage: 10, CurrentPage: 1, TotalCounts: 0, TotalPage: 1 };

    Selectd: number = 0;
    Options: object[] = [{
        value: 0,
        text: '媒體報導'
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
            StartOnlineDateTime: null,
            EndOnlineDateTime: null
        }
        _this.GetLatestNewsList(_this.searchmodel);
    }

    GetLatestNewsList(searchmodel: SearchModel) {
        const _this = this;
        const sendPagination: SendPaginationModel = {
            PerPage: _this.Pagination.PerPage,
            CurrentPage: _this.Pagination.CurrentPage
        }
        _this.DefaultLatestNewsListItem(searchmodel, sendPagination);       
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
            StartOnlineDateTime: moment(_this.StartOnlineDateTime).startOf('day').toDate(),
            EndOnlineDateTime: moment(_this.EndOnlineDateTime).endOf('day').toDate()
        }

        console.log(_this.searchmodel);

        _this.GetLatestNewsListItem(_this.searchmodel, sendPagination);
    }

    DefaultLatestNewsListItem(searchmodel, sendPagination) {
        const _this = this;

        service.GetLatestNewsList(searchmodel, sendPagination).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.ListItem = res.Data;
                _this.Pagination = res.Pagination;

                _this.LimitDate(res.Data);
                //_this.CheckOnlineDate();

            }
        }).catch(err => {
            console.log(err);
        })
    }

    GetLatestNewsListItem(searchmodel, sendPagination) {
        const _this = this;

        service.GetLatestNewsList(searchmodel, sendPagination).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.ListItem = res.Data;
                _this.Pagination = res.Pagination;
                //_this.CheckOnlineDate();

            }
        }).catch(err => {
            console.log(err);
        })
    }

    LimitDate(Data) {
        const _this = this;

        var MaxDate = Data.map(s => moment(s.CreateTime));
        _this.EndDateTime = moment.max(MaxDate).format("YYYY-MM-DD");
        _this.MaxDate = _this.EndDateTime;

        var MinDate = Data.map(s => moment(s.CreateTime));
        _this.StartDateTime = moment.min(MinDate).format("YYYY-MM-DD");
        _this.MinDate = _this.StartDateTime;

        var OnlineMaxDate = Data.map(s => moment(s.StartDateTime));
        _this.EndOnlineDateTime = moment.max(OnlineMaxDate).format("YYYY-MM-DD");
        _this.OnlineMaxDate = _this.EndOnlineDateTime;

        var OnlineMinDate = Data.map(s => moment(s.StartDateTime));
        _this.StartOnlineDateTime = moment.min(OnlineMinDate).format("YYYY-MM-DD");
        _this.OnlineMinDate = _this.StartOnlineDateTime;
    }

    @Watch('ListItem')
    GetImageUrl() {
        const _this = this;
        if (_this.ListItem) {
            const BasePath = window.BasePath; // _Layout.cshtml
            var length = _this.ListItem.length;
            for (var i = 0; i < length; i++) {
                _this.ListItem[i].ImageName = BasePath + UrlPathEnum.LatestNewsPhoto + '?filename=' + _this.ListItem[i].ImageName;
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
        _this.searchmodel.Query = _this.Query;

        _this.searchmodel.StartDateTime = moment(_this.StartDateTime).startOf('day').toDate();
        _this.GetLatestNewsList(_this.searchmodel);
    }

    GetEditLatestNews(LatestNewsId: number) {
        var url = '/LatestNews/Edit?LatestNewsId=' + LatestNewsId;
        window.location.href = url;
    }

    DeleteLatestNews(LatestNewsId: number) {
        const _this = this;
        service.DeleteLatestNews(LatestNewsId).then(res => {
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
                    variant: 'success',
                })
                _this.SetSearchDate();
            }
        }).catch(err => {
            console.log(err);
            _this.$bvToast.toast('與伺服器連接發生錯誤', {
                title: '前台首頁圖片',
                variant: 'danger',
            })
        })    }
}
