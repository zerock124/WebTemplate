import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import service from './service';
import { StaticPageViewModel } from './model'
import moment = require('moment');

@Component({
    template: '#StaticPageItem',
})

export default class StaticPageItem extends Vue {
    @Prop(Object) ListItem: StaticPageViewModel | undefined;

    PageContent: string = '';
    StaticPageItem: StaticPageViewModel | null = null;

    SubmitCompanyProfile() {
        const _this = this;
        _this.SubmitStaticPage(_this.PageContent);
    }

    SubmitStaticPage(data) {
        const _this = this;
        if (_this.ListItem) {
            _this.ListItem.PageContent = data;

            service.EditStaticPage(_this.ListItem).then(res => {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.$bvToast.toast("變更'公司介紹'成功", {
                        title: '變更靜態頁',
                        autoHideDelay: 50,
                        variant:'success'
                    })
                }
            }).catch(err => {
                console.log(err);
            })
        }
    }

    /**
     * 新增靜態頁內容
     * @param data
     */
    //SubmitService(data) {
    //    const _this = this;
    //    _this.StaticPageItem = {
    //        StaticPageId: 1,
    //        StaticPageEnum: 3,
    //        PageContent: data,
    //        CreateTime: moment().toDate(),
    //        CreateUser: '',
    //        UpdateTime: moment().toDate(),
    //        UpdateUser: ''
    //    }
    //    service.CreateStaticPage(_this.StaticPageItem).then(res => {
    //        if (!res.Success) {
    //            console.log(res);
    //        }
    //        if (res.Data) {
    //            _this.StaticPageItem = res.Data
    //        }
    //    }).catch(err => {
    //        console.log(err);
    //    })
    //}

    @Watch('ListItem')
    OnListItemChange() {
        const _this = this;
        if (_this.ListItem) {
            _this.PageContent = _this.ListItem.PageContent;
        }
    }
}
