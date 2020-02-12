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
    Query: string = '';

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
        text: '圖片連結'
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
        }
        _this.GetFontHomeList(_this.searchmodel);
    }

    GetFontHomeList(searchmodel: SearchModel) {
        const _this = this;
        const sendPagination: SendPaginationModel = {
            PerPage: _this.Pagination.PerPage,
            CurrentPage: _this.Pagination.CurrentPage
        }
        _this.GetFontHomeListItem(searchmodel, sendPagination);
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
            Query: _this.Query
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
            }
        }).catch(err => {
            console.log(err);
        })
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

    @Watch('RideRecordList')
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
        _this.searchmodel.EndDateTime = moment(_this.EndDateTime).startOf('day').toDate();
        _this.GetFontHomeList(_this.searchmodel);
    }

    GetEditFontHome(FontHomeId: number) {
        console.log("a");
        var url = '/FontHome/GetEditFontHome?FontHomeId=' + FontHomeId;
        window.location.href = url;
    }
}