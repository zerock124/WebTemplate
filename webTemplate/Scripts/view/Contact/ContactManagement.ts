import { Vue, Component, Watch, Emit, Prop } from 'vue-property-decorator'
import { ContactViewModel, SearchModel } from './model'
import service from './service'
import moment = require('moment')
import { UrlPathEnum } from '../Share/Enums'
import { dateToDateTimeString, GetSexEnum, GetContactEnum, GetContactStatus } from "../Share/FilterFunction";

@Component({
    template: '#ContactManagement',
    filters: {
        dateToDateTimeString,
        GetSexEnum,
        GetContactEnum,
        GetContactStatus
    }
})


export default class ContactManagement extends Vue {
    ListItem: ContactViewModel[] = []

    StartDateTime: string = moment().startOf('day').format('YYYY-MM-DD');
    EndDateTime: string = moment().endOf('day').format('YYYY-MM-DD');
    OnlineDateTime: string = moment().format('YYYY-MM-DD');
    Query: string = '';

    MaxDate: string = '';
    MinDate: string = '';

    PerPage: number = 10;
    CurrentPage: number = 1;
    TotalPage: number = 1;
    TotalCounts: number = 0;

    searchmodel: SearchModel | null = null

    Pagination: PaginationViewModel = { PerPage: 10, CurrentPage: 1, TotalCounts: 0, TotalPage: 1 };

    Selectd: number = 0;
    Options: object[] = [{
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
        _this.GetContactList(_this.searchmodel);
    }

    GetContactList(searchmodel: SearchModel) {
        const _this = this;
        const sendPagination: SendPaginationModel = {
            PerPage: _this.Pagination.PerPage,
            CurrentPage: _this.Pagination.CurrentPage
        }
        _this.DefaultContactListItem(searchmodel, sendPagination);
    }

    DefaultContactListItem(searchmodel, sendPagination) {
        const _this = this;

        service.GetContactList(searchmodel, sendPagination).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.ListItem = res.Data;
                _this.Pagination = res.Pagination;

                _this.LimitDate(res);

            }
        }).catch(err => {
            console.log(err);
        })
    }

    LimitDate(Data) {
        const _this = this;
        console.log(Data);
        _this.StartDateTime = moment(Data.MinDateTime).startOf('day').format("YYYY-MM-DD");
        _this.EndDateTime = moment(Data.MaxDateTime).endOf('day').format("YYYY-MM-DD");
        _this.MinDate = _this.StartDateTime;
        _this.MaxDate = _this.EndDateTime;
    }

    SetSearchDate() {
        const _this = this;
        _this.Pagination.PerPage = 10;
        _this.Pagination.CurrentPage = 1;
        const sendPagination: SendPaginationModel = {
            PerPage: _this.Pagination.PerPage,
            CurrentPage: _this.Pagination.CurrentPage
        }

        _this.searchmodel = {
            SearchEnum: _this.Selectd,
            StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
            EndDateTime: moment(_this.EndDateTime).endOf('day').toDate(),
            Query: _this.Query,
        }

        console.log(_this.searchmodel);

        _this.GetContactListItem(_this.searchmodel, sendPagination);
    }

    GetContactListItem(searchmodel, sendPagination) {
        const _this = this;

        service.GetContactList(searchmodel, sendPagination).then(res => {
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


    //@Watch('PerPage')
    //OnPerPageChange() {
    //    this.SetSendPagination();
    //}
    //@Watch('CurrentPage')
    //OnCurrentPageChange() {
    //    this.SetSendPagination();
    //}

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
            _this.GetContactList(_this.searchmodel);
        }
    }

    GetEditContact(ContactId: number) {
        var baseurl = window.BasePath;
        console.log(baseurl);
        var url = baseurl + 'Contact/Edit?ContactId=' + ContactId;
        window.location.href = url;
    }

    DeleteContact(ContactId: number) {
        const _this = this;
        service.DeleteContact(ContactId).then(res => {
            if (!res.Success) {
                console.log(res);
                _this.$bvToast.toast('刪除聯絡紀錄失敗', {
                    title: '聯絡我們管理',
                    variant: 'warning',
                })
            }
            if (res.Success) {
                _this.$bvToast.toast('刪除聯絡紀錄成功', {
                    title: '聯絡我們管理',
                    variant: 'success',
                })
                _this.SetSearchDate();
            }
        }).catch(err => {
            console.log(err);
            _this.$bvToast.toast('與伺服器連接發生錯誤', {
                title: '聯絡我們管理',
                variant: 'danger',
            })
        })
    }
}