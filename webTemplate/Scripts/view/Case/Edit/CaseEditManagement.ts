import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import { CaseViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import service from '../service'
import moment = require('moment');

@Component({
    template: '#CaseEditManagement'
})

export default class CaseEditManagement extends Vue {

    httpURL: string = window.location.href;

    CaseItem: CaseViewModel | null = null;

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

    CaseId: number = 0;

    created() {
        const _this = this;
        _this.GetCaseId();
        _this.GetCaseItem();
    }

    GetCaseId() {
        const _this = this;
        let httpURL = window.location.href;
        _this.CaseId = parseInt(httpURL.split("?CaseId=")[1]);
    }

    GetCaseItem() {
        const _this = this;
        service.GetEditCaseItem(_this.CaseId).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.CaseItem = res.Data;
                _this.CaseId = res.Data.CaseId;
                _this.ImageName = res.Data.ImageName;
                _this.CaseUrl = res.Data.CaseUrl;
                _this.CaseName = res.Data.CaseName;
                _this.CaseContent = res.Data.CaseContent;
                _this.CaseEnum = res.Data.CaseEnum;
                _this.Status = res.Data.Status;
                const photo = _this.ImageName;
                const BasePath = window.BasePath; // _Layout.cshtml
                _this.DefaultImage = BasePath + UrlPathEnum.CasePhoto + '?filename=' + photo;
            }
        }).catch(err => {
            console.log(err);
        })
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

    SetEditFontHome() {
        const _this = this;
        if (_this.CaseItem) {
            const {
                CaseId,
                PhotoFile,
                ImageName,
                CaseUrl,
                CaseName,
                CaseContent,
                CaseEnum,
                Status,
            } = this;

            const _formdata = new FormData();
            _formdata.append('CaseId', CaseId.toString())
            _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '')
            _formdata.append('ImageName', ImageName)
            _formdata.append('CaseUrl', CaseUrl)
            _formdata.append('CaseName', CaseName)
            _formdata.append('CaseName', CaseName)
            _formdata.append('CaseContent', CaseContent)
            _formdata.append('CaseEnum', CaseEnum.toString())
            _formdata.append('Status', JSON.stringify(Status))

            _this.EditCase(_formdata);

        }
    }

    EditCase(data) {
        service.EditCaseItem(data).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Success) {
                const locationURL = this.httpURL.split("/Edit?")[0];
                console.log(locationURL);
                document.location.href = locationURL;
            }
        }).catch(err => {
            console.log(err);
        })
    }

    @Watch('Status')
    OnStatusChange() {
        this.$emit('change', { value: this.Status, srcEvent: event });
        this.$emit('input', this.Status);
        console.log(this.Status);
    }
}