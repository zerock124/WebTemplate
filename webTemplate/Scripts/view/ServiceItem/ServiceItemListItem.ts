import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import { ServiceItemViewModel } from './model'
import { UrlPathEnum } from '../Share/Enums';
import serviceItem_event from './ServiceItemEvent'

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
        if (_this.ListItem) {
            const BasePath = window.BasePath; // _Layout.cshtml
            const photo = _this.ListItem.ImageName;
            _this.DefaultImage = BasePath + UrlPathEnum.ServiceItemPhoto + '?filename=' + photo;
        }
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

    @Watch('ServiceItemName')
    OnServiceItemNameChange() {
        const _this = this;
        if (_this.ListItem) {
            const ServiceItem = {
                ServiceItemId: _this.ListItem.ServiceItemId,
                ImageName: _this.ImageName,
                PhotoFile: _this.PhotoFile,
                ServiceItemName: _this.ServiceItemName
            }
            serviceItem_event.$emit('EmitServiceItem', ServiceItem);
        }
    }

    @Watch('PhotoFile')
    OnListItemChange() {
        const _this = this;
        if (_this.ListItem) {
            const ServiceItem = {
                ServiceItemId: _this.ListItem.ServiceItemId,
                ImageName: _this.ImageName,
                PhotoFile: _this.PhotoFile,
                ServiceItemName: _this.ServiceItemName
            }
            serviceItem_event.$emit('EmitServiceItem', ServiceItem);
        }
    }
}