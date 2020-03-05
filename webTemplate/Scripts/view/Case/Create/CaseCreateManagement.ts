import { Vue, Component, Prop } from 'vue-property-decorator'
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');
import VueEditor from 'vue2-editor'
import InputTag from 'vue-input-tag';
import service from '../service';

Vue.use(VueEditor);

@Component({
    template: '#CaseCreateManagement',
    components: {
        'input-tag': InputTag
    }
})

export default class CaseCreateManagement extends Vue {

    httpURL: string = window.location.href;

    image: string = '';

    PhotoFile: File | null = null;
    DefaultImage: string = '';

    ImageName: string = '';
    CaseEnum: number = 0;

    LimitNumber: number = 10;
    tags: string[] = [];

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

    SaveForm: string = 'Loading';

    customToolbar = [
        ["bold", "italic", "underline"],
        [{ list: "ordered" },
        { list: "bullet" }]
    ]

    created() {
        const _this = this;
        _this.GetDefaultCaseUrl();
    }

    SetCreateCase() {
        const _this = this;
        _this.$bvModal.show('CaseModal');
        _this.SaveForm = 'Loading';
            const {
                PhotoFile,
                ImageName,
                CaseUrl,
                CaseName,
                CaseContent,
                CaseEnum,
                Status,
                tags
            } = this;

            const _formdata = new FormData();
            _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '')
            _formdata.append('ImageName', ImageName)
            _formdata.append('CaseUrl', CaseUrl)
            _formdata.append('CaseName', CaseName)
            _formdata.append('CaseContent', CaseContent)
            _formdata.append('CaseEnum', CaseEnum.toString())
            _formdata.append('Status', JSON.stringify(Status))
            _formdata.append('LabelTag', tags.toString())


            _this.CreateCase(_formdata);
    }

    CreateCase(data) {
        const _this = this;
        service.CreateCaseItem(data).then(res => {
            if (!res.Success) {
                _this.SaveForm = 'Error';
                console.log(res);
            }
            if (res.Success) {
                _this.SaveForm = 'Success';
                
            }
        }).catch(err => {
            _this.SaveForm = 'Error';
            console.log(err);
        })
    }

    HideModal() {
        const _this = this;
        _this.$bvModal.hide('CaseModal');
    }

    CloseModal() {
        const _this = this;
        _this.$bvModal.hide('CaseModal');
        const locationURL = this.httpURL.split("/Create")[0];
        document.location.href = locationURL;
    }

    GetDefaultCaseUrl() {
        const _this = this;
        const BasePath = window.BasePath; // _Layout.cshtml
        const photo = "NoImage.jpg";
        _this.DefaultImage = BasePath + UrlPathEnum.LatestNewsPhoto + '?filename=' + photo;
    };

    fileSelected(event) {
        const file = event.target.files.item(0); //取得File物件
        this.ImageName = file.name;
        const reader = new FileReader(); //建立FileReader 監聽 Load 事件
        reader.addEventListener('load', this.imageLoader);
        reader.readAsDataURL(file);
    };

    imageLoader(event) {
        this.image = event.target.result;
    }
}