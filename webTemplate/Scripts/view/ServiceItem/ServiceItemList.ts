import { Vue, Component } from 'vue-property-decorator'
import { UrlPathEnum } from '../Share/Enums';
import { ServiceItemViewModel } from './model';
import service from './service'
import ServiceItemListItem from './ServiceItemListItem';
import moment = require('moment');

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
            UpdateUser: ''
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
            }
        }).catch(err => {
            console.log(err);
        })
    }
}