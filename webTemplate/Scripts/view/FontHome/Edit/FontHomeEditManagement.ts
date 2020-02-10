import { Vue, Component, Prop } from 'vue-property-decorator'
import { FontHomeViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import service from '../service'
import moment = require('moment');

@Component({
    template: '#FontHomeEditManagement'
})

export default class FontHomeEditManagement extends Vue {

    httpURL: string = window.location.href;

    image: string = '';

    FontHomeItem: FontHomeViewModel | null = null;

    PhotoFile: File | null = null;
    ImageName: string = '';
    FontHomeUrl: string = '';
    StartDateTime: string = moment().startOf('day').format("YYYY-MM-DD HH:mm:ss");
    EndDateTime: string = moment().endOf('day').format("YYYY-MM-DD HH:mm:ss");
    Remark: string = '';
    Status: boolean = false;

    FontHomeId: number = 0;

    FontHomeImage: string = '';

    created() {
        const _this = this;
        _this.GetFontHomeId();
        _this.GetFontHomeItem();
    }

    GetFontHomeId() {
        const _this = this;
        let httpURL = window.location.href;
        _this.FontHomeId = parseInt(httpURL.split("?FontHomeId=")[1]);
    }

    GetFontHomeItem() {
        const _this = this;
        service.GetEditFontHomeItem(_this.FontHomeId).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.FontHomeItem = res.Data;
                _this.ImageName = res.Data.ImageName;
                _this.FontHomeUrl = res.Data.FontHomeUrl;
                _this.StartDateTime = moment(res.Data.StartDateTime).format("YYYY-MM- DD HH:mm:ss");
                _this.EndDateTime = moment(res.Data.EndDateTime).format("YYYY-MM- DD HH:mm:ss");
                _this.Remark = res.Data.Remark;
                _this.Status = res.Data.Status;
                const photo = _this.ImageName;
                _this.FontHomeImage = UrlPathEnum.FontHomePhoto + '?filename=' + photo;
            }
        }).catch(err => {
            console.log(err);
        })
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

    SetEditFontHome() {
        const _this = this;
        if (_this.FontHomeItem) {
            const {
                FontHomeId,
                PhotoFile,
                ImageName,
                FontHomeUrl,
                StartDateTime,
                EndDateTime,
                Status,
                Remark,
            } = this;

            const _formdata = new FormData();
            _formdata.append('FontHomeId', FontHomeId.toString())
            _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '')
            _formdata.append('ImageName', ImageName)
            _formdata.append('FontHomeUrl', FontHomeUrl)
            _formdata.append('StartDateTime', StartDateTime)
            _formdata.append('EndDateTime', EndDateTime)
            _formdata.append('Remark', Remark)
            _formdata.append('Status', JSON.stringify(Status))

            _this.EditFontHome(_formdata);

        }
    }

    EditFontHome(data) {
        service.EditFontHomeItem(data).then(res => {
            if (!res.Success) {
            }
            if (res.Data) {
            }
        }).catch(err => {
            console.log(err);
        })
    }
}