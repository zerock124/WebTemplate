import { Vue, Component, Prop } from 'vue-property-decorator'
import { CaseViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');
import VueEditor from 'vue2-editor'

Vue.use(VueEditor);

@Component({
    template: '#CaseCreateManagement'
})

export default class CaseCreateManagement extends Vue {

    image: string = '';

    PhotoFile: File | null = null;
    DefaultImage: string = '';

    ImageName: string = '';
    CaseEnum: number = 0;

    Options: object[] = [{
        value: 0,
        text: '行銷活動'
    }, {
        value: 1,
        text: '臉書機器人'
    }, {
        value: 2,
        text: '官網'
        }];

    CaseUrl: string = '';
    CaseName: string = '';
    CaseContent: string = '';
    Status: boolean = false;

    customToolbar = [
        ["bold", "italic", "underline"],
        [{ list: "ordered" },
        { list: "bullet" }]
    ]

    created() {
        const _this = this;
        _this.GetDefaultCaseUrl();
    }

    GetDefaultCaseUrl() {
        const _this = this;
        const BasePath = window.BasePath; // _Layout.cshtml
        const photo = "NoImage.jpg";
        _this.DefaultImage = BasePath + UrlPathEnum.LatestNewsPhoto + '?filename=' + photo;
    };

    fileSelected(event) {
        const file = event.target.files.item(0); //取得File物件
        console.log(file);
        this.ImageName = file.name;
        const reader = new FileReader(); //建立FileReader 監聽 Load 事件
        reader.addEventListener('load', this.imageLoader);
        reader.readAsDataURL(file);
    };

    imageLoader(event) {
        this.image = event.target.result;
    }
}