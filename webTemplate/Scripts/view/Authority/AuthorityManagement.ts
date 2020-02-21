import { Vue, Component, Watch, Emit, Prop } from 'vue-property-decorator'
import { AuthorityViewModel, SearchModel } from './model'
import service from './service'
import moment = require('moment')
import { UrlPathEnum } from '../Share/Enums'
import { dateToDateTimeString, GetLatestNewsEnum } from "../Share/FilterFunction";

@Component({
    template: '#AuthorityManagement',
    filters: {
        dateToDateTimeString,
        GetLatestNewsEnum
    }
})


export default class AuthorityManagement extends Vue {
    ListItem: AuthorityViewModel[] = []

    StartDateTime: string = moment().startOf('day').format('YYYY-MM-DD');
    EndDateTime: string = moment().endOf('day').format('YYYY-MM-DD');
    StartOnlineDateTime: string = moment().format('YYYY-MM-DD');
    EndOnlineDateTime: string = moment().format('YYYY-MM-DD');
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
        text: '帳號'
    }, {
        value: 1,
        text: '角色'
    }, {
        value: 2,
        text: '建立者'
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
        _this.GetAuthorityList(_this.searchmodel);
    }

    GetAuthorityList(searchmodel: SearchModel) {
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
        }

        _this.GetAuthorityListItem(_this.searchmodel, sendPagination);
    }

    DefaultLatestNewsListItem(searchmodel, sendPagination) {
        const _this = this;

        service.GetAuthorityList(searchmodel, sendPagination).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.ListItem = res.Data;
                _this.Pagination = res.Pagination;

                _this.LimitDate(res.Data);

            }
        }).catch(err => {
            console.log(err);
        })
    }

    GetAuthorityListItem(searchmodel, sendPagination) {
        const _this = this;

        service.GetAuthorityList(searchmodel, sendPagination).then(res => {
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

    LimitDate(Data) {
        const _this = this;
        _this.StartDateTime = moment(Data.MinDateTime).startOf('day').format("YYYY-MM-DD");
        _this.EndDateTime = moment(Data.MaxDateTime).endOf('day').format("YYYY-MM-DD");
        _this.MinDate = _this.StartDateTime;
        _this.MaxDate = _this.EndDateTime;
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
        _this.searchmodel.EndDateTime = moment(_this.EndDateTime).endOf('day').toDate();

        _this.GetAuthorityList(_this.searchmodel);
    }

    GetEditAuthority(Id: string) {
        const _this =this
        service.CheckAuthority(Id).then(res => {
            if (!res.Success) {
                _this.$bvToast.toast(res.Message, {
                    title: '權限管理',
                    variant:'warning'
                })
            }
            if (res.Success) {
                var url = '/Authority/Edit?Id=' + Id;
                window.location.href = url;
            }
        }).catch(err => {
            console.log(err);
        })
    }

    DeleteAuthority(Id: string) {
        const _this = this;
        console.log(Id);
        service.DeleteAuthorityItem(Id).then(res => {
            if (!res.Success) {
                console.log(res);
                _this.$bvToast.toast('刪除帳號失敗', {
                    title: '權限管理',
                    variant: 'warning',
                })
            }
            if (res.Success) {
                _this.$bvToast.toast('刪除帳號成功', {
                    title: '權限管理',
                    variant: 'success',
                })
                _this.SetSearchDate();
            }
        }).catch(err => {
            console.log(err);
            _this.$bvToast.toast('與伺服器連接發生錯誤', {
                title: '權限管理',
                variant: 'danger',
            })
        })
    }
}
