import { Vue, Component, Prop } from 'vue-property-decorator'
import { ServiceItemViewModel } from './model'
import { UrlPathEnum } from '../Share/Enums';

@Component({
    template: '#ServiceItemListItem'
})

export default class ServiceItemListItem extends Vue {
    @Prop(Object) ListItem: ServiceItemViewModel | undefined

    image: string = '';
    ImageName: string = '';
    PhotoFile: File | null = null;
    ServiceItemName: string = '';
    DefaultImage: string = '';

    created() {
        const _this = this;
        _this.GetDefaultFontHomeUrl();
    }

    GetDefaultFontHomeUrl() {
        const _this = this;
        const BasePath = window.BasePath; // _Layout.cshtml
        const photo = "NoImage.png";
        _this.DefaultImage = BasePath + UrlPathEnum.ServiceItemPhoto + '?filename=' + photo;
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