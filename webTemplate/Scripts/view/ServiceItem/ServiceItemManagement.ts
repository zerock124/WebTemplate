import { Vue, Component } from 'vue-property-decorator'
import ServiceItemList from './ServiceItemList';
import { UrlPathEnum } from '../Share/Enums';

@Component({
    template: '#ServiceItemManagement',
    components: {
        'service-item-list': ServiceItemList
    }
})

export default class ServiceItemManagement extends Vue {


}