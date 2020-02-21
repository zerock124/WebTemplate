import { Vue, Component, Watch, Emit, Prop } from 'vue-property-decorator'
import { IWebConfigService, WebConfigViewModel } from './model'
import service from './service'
import moment = require('moment')
import { UrlPathEnum } from '../Share/Enums'
import $ from 'jQuery';

@Component({
    template: '#WebConfigManagement',
})


export default class WebConfigManagement extends Vue {
    WebConfigItem: WebConfigViewModel | null = null;

    SaveForm: string = 'Loading';

    created() {
        const _this = this;
        _this.GetWebConfig();
    }

    GetWebConfig() {
        const _this = this;
        service.GetWebConfig(1).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.WebConfigItem = res.Data;
            }
        }).catch(err => {
            console.log(err);
        })
    }

    SubmitWebConfig() {
        const _this = this;
        _this.SaveForm = "Loading";
        _this.$bvModal.show("WebConfigModal");
        if (_this.WebConfigItem) {
            service.EditWebConfig(_this.WebConfigItem).then(res => {
                if (!res.Success) {
                    console.log(res);
                    _this.SaveForm = "Error";
                }
                if (res.Success) {
                    _this.SaveSuccess();
                }
            }).catch(err => {
                console.log(err);
                _this.SaveForm = "Error";
            })
        }
    }

    SaveSuccess() {
        const _this = this;
        setTimeout(function () {
            _this.SaveForm ="Success"
        }, 1000);
    }

    CloseModel() {
        const _this = this;
        _this.$bvModal.hide("WebConfigModal");
    }
}