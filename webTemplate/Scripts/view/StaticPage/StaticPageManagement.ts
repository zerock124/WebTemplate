import { Vue, Component } from 'vue-property-decorator'
import StaticPageItem from './StaticPageItem';
import VueEditor from 'vue2-editor'
import service from './service'
import { StaticPageViewModel } from './model'

Vue.use(VueEditor);

@Component({
    template: '#StaticPageManagement',
    components: {
        'company-profile': StaticPageItem,
        'technology-development': StaticPageItem,
        'contact-method': StaticPageItem,
        'privacy-policy': StaticPageItem
    }
})

export default class StaticPageManagement extends Vue {
    StaticPageList: StaticPageViewModel[] = []
    CompanyProfile: StaticPageViewModel | null = null
    TechnologyDevelopment: StaticPageViewModel | null = null
    ContactMethod: StaticPageViewModel | null = null
    PrivacyPolicy: StaticPageViewModel | null = null

    created() {
        const _this = this;
        _this.GetStaticPageList();
    }


    GetStaticPageList() {
        const _this = this;
        service.GetStaticPageList().then(res => {
            if (res.Success) {

            }
            if (res.Data) {
                _this.StaticPageList = res.Data;
                const length = _this.StaticPageList.length
                for (var i = 0; i < length; i++) {
                    switch (_this.StaticPageList[i].StaticPageEnum) {
                        case 0:
                            _this.CompanyProfile = _this.StaticPageList[i];
                            break;
                        case 1:
                            _this.TechnologyDevelopment = _this.StaticPageList[i]
                            break;
                        case 2:
                            _this.ContactMethod = _this.StaticPageList[i]
                            break;
                        case 3:
                            _this.PrivacyPolicy = _this.StaticPageList[i]
                            break;
                        default:
                    }
                }
            }
        }).catch(err => {
            console.log(err);
        })
    }
}