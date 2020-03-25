import { Vue, Component } from 'vue-property-decorator'
import { UrlPathEnum } from '../Share/Enums';
import { ServiceItemViewModel } from './model';
import service from './service'
import ServiceItemListItem from './ServiceItemListItem';
import moment = require('moment');
import serviceItem_event from './ServiceItemEvent'

@Component({
    template: '#ServiceItemList',
    components: {
        'service-item-list-item': ServiceItemListItem
    }
})

export default class ServiceItemList extends Vue {
    ServiceItemList: ServiceItemViewModel[] = [];
    Id: number = 0;

    created() {
        const _this = this;
        _this.GetServiceItemList();
        
        serviceItem_event.$on('EmitServiceItem', _this.EmitServiceItem.bind(_this));
        serviceItem_event.$on('GetServiceItemList', _this.GetServiceItemList);
    }

    beforeDestroy() {
        serviceItem_event.$off('EmitServiceItem');
        serviceItem_event.$off('GetServiceItemList');
    }

    CreateSpaceItem() {
        const _this = this;
        _this.Id += 1;
        const spaceItem: ServiceItemViewModel = {
            ServiceItemId: _this.Id,
            ServiceItemName: '',
            ImageName: 'NoImage.png',
            CreateTime: moment().toDate(),
            CreateUser: '',
            UpdateTime: moment().toDate(),
            UpdateUser: '',
            PhotoFile: null,
            IconName: '',
            Mode: 'Images'
        }
        _this.ServiceItemList.push(spaceItem);
    }

    GetServiceItemList() {
        const _this = this;
        service.GetServiceItemList().then(res => {
            if (!res.Success) {
            }
            if (res.Data) {
                _this.ServiceItemList = res.Data
                var length = _this.ServiceItemList.length - 1;
                _this.Id = _this.ServiceItemList[length].ServiceItemId;
            }
        }).catch(err => {
            console.log(err);
        })
    }

    EmitServiceItem(data) {
        const _this = this;
        if (_this.ServiceItemList) {
            const length = _this.ServiceItemList.length;
            for (var i = 0; i < length; i++) {
                if (_this.ServiceItemList[i].ServiceItemId == data.ServiceItemId) {
                    _this.ServiceItemList[i].PhotoFile = data.PhotoFile;
                    _this.ServiceItemList[i].ImageName = data.ImageName;
                    _this.ServiceItemList[i].ServiceItemName = data.ServiceItemName;
                    _this.ServiceItemList[i].IconName = data.IconName;
                    _this.ServiceItemList[i].Mode = data.Mode;
                }
            }
        }
    }

    CreateFormDateList() {
        const _this = this;
        if (_this.ServiceItemList) {
            const _formdate = new FormData();
            const length = _this.ServiceItemList.length;
            for (var i = 0; i < length; i++) {

                let photoFile: File | null = null;
                _formdate.append('model[' + i + '].ServiceItemId', _this.ServiceItemList[i].ServiceItemId.toString());
                _formdate.append('model[' + i + '].ServiceItemName', _this.ServiceItemList[i].ServiceItemName);
                _formdate.append('model[' + i + '].ImageName', _this.ServiceItemList[i].ImageName);

                _formdate.append('model[' + i + '].IconName', _this.ServiceItemList[i].IconName);
                _formdate.append('model[' + i + '].Mode', _this.ServiceItemList[i].Mode);

                photoFile = _this.ServiceItemList[i].PhotoFile;
                _formdate.append('model[' + i + '].PhotoFile', photoFile ? photoFile : '');
            }
            _this.CreateServiceItemList(_formdate);
        }
    }

    CreateServiceItemList(_formdate) {
        const _this = this;
        service.CreateServiceItemList(_formdate).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Success) {
                _this.$bvToast.toast("服務項目新增成功", {
                    title: '服務項目管理',
                    variant: 'success'
                })
            }
        }).catch(err => {
            console.log(err);
        })
    }
}