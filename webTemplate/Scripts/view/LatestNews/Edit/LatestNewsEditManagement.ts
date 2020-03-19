import { Vue, Component, Prop } from 'vue-property-decorator'
import { LatestNewsViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import service from '../service'
import moment = require('moment');
import VueEditor from 'vue2-editor'
import InputTag from 'vue-input-tag';

Vue.use(VueEditor);

@Component({
    template: '#LatestNewsEditManagement',
    components: {
        'input-tag': InputTag
    }
})

export default class LatestNewsEditManagement extends Vue {

    httpURL: string = window.location.href;

    image: string = '';

    LatestNewsItem: LatestNewsViewModel | null = null;

    PhotoFile: File | null = null;
    ImageName: string = '';
    LatestNewsEnum: number = 0;
    StartDateTime: string = moment().startOf('day').format("YYYY-MM-DD HH:mm:ss");
    LatestNewsTitle: string = '';
    LatestNewsContent: string = '';
    Remark: string = '';
    Status: boolean = false;

    Options: object[] = [{
        value: 0,
        text: '媒體報導'
    }];

    LatestNewsId: number = 0;

    DefaultImage: string = '';

    LimitNumber: number = 10;
    tags: string[] = [];

    customToolbar = [
        ["bold", "italic", "underline"],
        [{ list: "ordered" },
        { list: "bullet" }]
    ]

    SaveForm: string = 'Loading';

    created() {
        const _this = this;
        _this.GetFontHomeId();
        _this.GetFontHomeItem();
    }

    GetFontHomeId() {
        const _this = this;
        let httpURL = window.location.href;
        _this.LatestNewsId = parseInt(httpURL.split("?LatestNewsId=")[1]);
    }

    GetFontHomeItem() {
        const _this = this;
        service.GetEditLatestNewsItem(_this.LatestNewsId).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.LatestNewsItem = res.Data;
                _this.ImageName = res.Data.ImageName;
                _this.StartDateTime = moment(res.Data.StartDateTime).format("YYYY-MM- DD HH:mm:ss");
                _this.LatestNewsTitle = res.Data.LatestNewsTitle;
                _this.LatestNewsContent = res.Data.LatestNewsContent;
                _this.Remark = res.Data.Remark;
                _this.Status = res.Data.Status;
                if (res.Data.LabelTag) {
                    _this.tags = res.Data.LabelTag.split(',');
                }
                const photo = _this.ImageName;
                const BasePath = window.BasePath; // _Layout.cshtml
                _this.DefaultImage = BasePath + UrlPathEnum.LatestNewsPhoto + '?filename=' + photo;
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
        _this.$bvModal.show('LatestNewsModal');
        _this.SaveForm = 'Loading';
        if (_this.LatestNewsItem) {
            const {
                LatestNewsId,
                PhotoFile,
                ImageName,
                LatestNewsEnum,
                StartDateTime,
                LatestNewsTitle,
                LatestNewsContent,
                Status,
                Remark,
                tags
            } = this;

            const _formdata = new FormData();
            _formdata.append('LatestNewsId', LatestNewsId.toString())
            _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '')
            _formdata.append('ImageName', ImageName)
            _formdata.append('LatestNewsEnum', LatestNewsEnum.toString())
            _formdata.append('StartDateTime', StartDateTime)
            _formdata.append('LatestNewsTitle', LatestNewsTitle)
            _formdata.append('LatestNewsContent', LatestNewsContent.toString())
            _formdata.append('Remark', Remark)
            _formdata.append('Status', JSON.stringify(Status))
            _formdata.append('LabelTag', tags.toString())

            _this.EditFontHome(_formdata);

        }
    }

    EditFontHome(data) {
        const _this = this;
        service.EditLatestNewsItem(data).then(res => {
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
        _this.$bvModal.hide('LatestNewsModal');
    }

    CloseModal() {
        const _this = this;
        _this.$bvModal.hide('LatestNewsModal');
        const locationURL = this.httpURL.split("/Edit?")[0];
        document.location.href = locationURL;
    }
}