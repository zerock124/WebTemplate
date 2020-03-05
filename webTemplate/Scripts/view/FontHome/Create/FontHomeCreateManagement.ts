import { Vue, Component, Prop } from 'vue-property-decorator'
import { FontHomeViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');
import service from '../service'

@Component({
    template: '#FontHomeCreateManagement'
})

export default class FontHomeCreateManagement extends Vue {
    httpURL: string = window.location.href;

    image: string = '';

    PhotoFile: File | null = null;
    ImageName: string = '';
    FontHomeUrl: string = '';
    StartDateTime: string = moment().startOf('day').format("YYYY-MM-DD HH:mm:ss");
    EndDateTime: string = moment().endOf('day').format("YYYY-MM-DD HH:mm:ss");
    Remark: string = '';
    Status: boolean = false;

    DefaultImage: string = '';

    SaveForm: string = 'Loading';

    created() {
        const _this = this;
        _this.GetDefaultFontHomeUrl();
    }

    GetDefaultFontHomeUrl() {
        const _this = this;
        const BasePath = window.BasePath; // _Layout.cshtml
        const photo = "NoImage.jpg";
        _this.DefaultImage = BasePath + UrlPathEnum.FontHomePhoto + '?filename=' + photo;
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

    SetCreateFontHome() {
        const _this = this;
        _this.$bvModal.show('FontHomeModal');
        _this.SaveForm = 'Loading';
        const {
            PhotoFile,
            ImageName,
            FontHomeUrl,
            StartDateTime,
            EndDateTime,
            Remark,
            Status
        } = this;

        const _formdata = new FormData();
        _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '')
        _formdata.append('ImageName', ImageName)
        _formdata.append('FontHomeUrl', FontHomeUrl)
        _formdata.append('StartDateTime', StartDateTime)
        _formdata.append('EndDateTime', EndDateTime)
        _formdata.append('Remark', Remark)
        _formdata.append('Status', JSON.stringify(Status))

        _this.CreateFontHome(_formdata);

    }

    CreateFontHome(data) {
        const _this = this;
        service.CreateFontHome(data).then(res => {
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
        _this.$bvModal.hide('FontHomeModal');
    }

    CloseModal() {
        const _this = this;
        _this.$bvModal.hide('FontHomeModal');
        const locationURL = this.httpURL.split("/Create")[0];
        document.location.href = locationURL;
    }

}