import { Vue, Component, Prop } from 'vue-property-decorator'
import { LatestNewsViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');
import VueEditor from 'vue2-editor'
import service from '../service'
import InputTag from 'vue-input-tag';

Vue.use(VueEditor);

@Component({
    template: '#LatestNewsCreateManagement',
    components: {
        'input-tag': InputTag
    }
})

export default class LatestNewsCreateManagement extends Vue {
    httpURL: string = window.location.href;

    image: string = '';

    PhotoFile: File | null = null;
    DefaultImage: string = '';

    ImageName: string = '';
    LatestNewsEnum: number = 0;

    LimitNumber: number = 10;
    tags: string[] = [];

    Options: object[] = [{
        value: 0,
        text: '媒體報導'
    }];

    StartDateTime: string = moment().startOf('day').format("YYYY-MM-DD HH:mm:ss");
    LatestNewsTitle: string = '';
    LatestNewsContent: string = '';
    Remark: string = '';
    Status: boolean = false;

    customToolbar = [
        ["bold", "italic", "underline"],
        [{ list: "ordered" },
        { list: "bullet" }]
    ]

    SaveForm: string = 'Loading';

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

    SetCreateLatestNews() {
        const _this = this;
        _this.$bvModal.show('LatestNewsModal');
        _this.SaveForm = 'Loading';
        const {
            PhotoFile,
            LatestNewsEnum,
            StartDateTime,
            LatestNewsTitle,
            LatestNewsContent,
            Remark,
            Status,
            tags
        } = this;

        const _formdata = new FormData();
        _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '')
        _formdata.append('LatestNewsEnum', LatestNewsEnum.toString())
        _formdata.append('StartDateTime', StartDateTime)
        _formdata.append('LatestNewsTitle', LatestNewsTitle)
        _formdata.append('LatestNewsContent', LatestNewsContent)
        _formdata.append('Remark', Remark)
        _formdata.append('Status', JSON.stringify(Status))
        _formdata.append('LabelTag', tags.toString())

        _this.CreateFontHome(_formdata);

    }

    CreateFontHome(data) {
        const _this = this;
        service.CreateLatestNews(data).then(res => {
            if (!res.Success) {
                _this.SaveForm = 'Error';
                console.log(res);
            }
            if (res.Data) {
                _this.SaveForm = 'Success';
            }
        }).catch(err => {
            _this.SaveForm = 'Error';
            console.log(err);
        })
    }

    HideModal() {
        const _this = this;
        _this.$bvModal.hide('LatestNewsModal');
    }

    CloseModal() {
        const _this = this;
        _this.$bvModal.hide('LatestNewsModal');
        const locationURL = this.httpURL.split("/Create")[0];
        document.location.href = locationURL;
    }

}