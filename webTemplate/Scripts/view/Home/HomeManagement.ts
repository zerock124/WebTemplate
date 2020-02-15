import { Vue, Component } from 'vue-property-decorator'
import { HomeViewModel } from './model'
import service from './service'

@Component({
    template: '#HomeManagement'
})

export default class HomeManagement extends Vue {
    DateNumber: HomeViewModel | null = null;

    created() {
        const _this = this;
        _this.GetHomeNumber();
    }

    GetHomeNumber() {
        const _this = this;
        service.GetHomeDate().then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.DateNumber = res.Data;
            }
        }).catch(err => {
            console.log(err);
        })
    }
}