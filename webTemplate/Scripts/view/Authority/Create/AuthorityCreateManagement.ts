import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import { AuthorityViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');
import service from '../service';

@Component({
    template: '#LatestNewsCreateManagement'
})

export default class AuthorityCreateManagement extends Vue {
    UserName: string = '';
    Password: string = '';
    ConfirmPassword: string = '';

    PasswordTypes: string = "password";
    ShowPassword: boolean = false;

    Select: string = '';

    RoleList: RoleList[] = [];

    Options: RoleOptions[] = [];

    created() {
        const _this = this;
        _this.GetRoleOptions();
    }

    GetRoleOptions() {
        const _this = this;
        service.GetRoleOptions().then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.RoleList = res.Data;
            }
        }).catch(err => {
            console.log(err);
        })
    }

    ChangePasswordType() {
        const _this = this;
        if (_this.PasswordTypes == "password") {
            _this.PasswordTypes = "text";
            _this.ShowPassword = true;
        }
        else {
            _this.PasswordTypes = "password";
            _this.ShowPassword = false;
        }
    }

    @Watch('RoleList')
    OnOptionsChange() {
        const _this = this;
        if (_this.RoleList) {
            const length = _this.RoleList.length;
            for (var i = 0; i < length; i++) {
                const role: RoleOptions = {
                    value : _this.RoleList[i].Id,
                    text : _this.RoleList[i].Name
                }
                _this.Options.push(role);
            }
            _this.Select = _this.Options[0].value;
        }
    }

    get VisibleStopAuthority() {
        return Boolean(this.UserName && this.Password == this.ConfirmPassword);
    }
}

export interface RoleList {
    Id: string;
    Name: string;
}

export interface RoleOptions {
    value: string;
    text: string;
}