import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import { ServiceItemViewModel } from './model'
import { UrlPathEnum } from '../Share/Enums';
import serviceItem_event from './ServiceItemEvent'
import service from './service';

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
    IconName: string = '';
    Mode: string = '';

    created() {
        const _this = this;
        _this.GetDefaultFontHomeUrl();
    }

    GetDefaultFontHomeUrl() {
        const _this = this;
        if (_this.ListItem) {
            const BasePath = window.BasePath; // _Layout.cshtml
            _this.ImageName = _this.ListItem.ImageName;
            _this.IconName = _this.ListItem.IconName;
            _this.ServiceItemName = _this.ListItem.ServiceItemName;
            _this.Mode = _this.ListItem.Mode;
            if (_this.Mode == "Images") {
                const photo = _this.ListItem.ImageName;
                _this.DefaultImage = BasePath + UrlPathEnum.ServiceItemPhoto + '?filename=' + photo;
            }
            if (_this.Mode == "Icon") {
                const photo = "NoImage.png";
                _this.DefaultImage = BasePath + UrlPathEnum.ServiceItemPhoto + '?filename=' + photo;
            }
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
                ServiceItemName: _this.ServiceItemName,
                IconName: _this.IconName,
                Mode: _this.Mode
            }
            serviceItem_event.$emit('EmitServiceItem', ServiceItem);
        }
    }

    @Watch('IconName')
    OnIconNameChange() {
        const _this = this;
        if (_this.ListItem) {
            const ServiceItem = {
                ServiceItemId: _this.ListItem.ServiceItemId,
                ImageName: _this.ImageName,
                PhotoFile: _this.PhotoFile,
                ServiceItemName: _this.ServiceItemName,
                IconName: _this.IconName,
                Mode: _this.Mode
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
                ServiceItemName: _this.ServiceItemName,
                IconName: _this.IconName,
                Mode: _this.Mode
            }
            serviceItem_event.$emit('EmitServiceItem', ServiceItem);
        }
    }

    ChangeMode(Mode: string) {
        const _this = this;
        _this.Mode = Mode;
    }

    DeleteServiceItem() {
        const _this = this;
        _this.$bvModal.msgBoxConfirm('確認是否刪除', {
            title: '服務項目管理',
            size: 'sm',
            buttonSize: 'sm',
            okVariant: 'danger',
            okTitle: 'YES',
            cancelTitle: 'NO',
            footerClass: 'p-2',
            hideHeaderClose: false,
            centered: true
        }).then(value => {
            if (value) {
                if (_this.ListItem) {
                    service.DeleteServiceItem(_this.ListItem).then(res => {
                        if (!res.Success) {
                            console.log(res);
                            _this.$bvToast.toast('刪除服務項目失敗', {
                                title: '服務項目管理',
                                variant: 'warning',
                            })
                        }
                        if (res.Success) {
                            _this.$bvToast.toast('刪除服務項目成功', {
                                title: '服務項目管理',
                                variant: 'success',
                            })

                            serviceItem_event.$emit('GetServiceItemList');
                        }
                    }).catch(err => {
                        console.log(err);
                        _this.$bvToast.toast('與伺服器連接發生錯誤', {
                            title: '服務項目管理',
                            variant: 'danger',
                        })
                    })
                }
            }
        })
    }
}
