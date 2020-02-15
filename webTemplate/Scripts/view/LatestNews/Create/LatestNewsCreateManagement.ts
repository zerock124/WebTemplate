import { Vue, Component, Prop } from 'vue-property-decorator'
import { LatestNewsViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');

@Component({
    template: '#LatestNewsCreateManagement'
})

export default class LatestNewsCreateManagement extends Vue {

    image: string = '';

    PhotoFile: File | null = null;
    DefaultImage: string = '';

    ImageName: string = '';
    LatestNewsEnum: number = 0;

    Options: object[] = [{
        value: 0,
        text: '媒體報導'
    }];

    StartDateTime: string = moment().startOf('day').format("YYYY-MM-DD HH:mm:ss");
    LatestNewsTitle: string = '';
    LatestNewsContent: string = '';
    Remark: string = '';
    Status: boolean = false;



    created() {
        const _this = this;
        _this.GetDefaultFontHomeUrl();
    }

    GetDefaultFontHomeUrl() {
        const _this = this;
        const BasePath = window.BasePath; // _Layout.cshtml
        const photo = "NoImage.jpg";
        _this.DefaultImage = BasePath + UrlPathEnum.CasePhoto + '?filename=' + photo;
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