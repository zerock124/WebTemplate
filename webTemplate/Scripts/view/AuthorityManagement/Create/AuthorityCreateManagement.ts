import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import { AuthorityViewModel, RegisterViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');
import service from '../service';

@Component({
    template: '#LatestNewsCreateManagement'
})

export default class AuthorityCreateManagement extends Vue {
    httpURL: string = window.location.href;

    UserName: string = '';
    Password: string = '';
    ConfirmPassword: string = '';

    PasswordTypes: string = "password";
    ShowPassword: boolean = false;

    RoleId: string = '';

    RoleList: RoleList[] = [];

    Options: RoleOptions[] = [];

    SaveForm: string = 'Loading';
    Message: string = '';

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
                    value: _this.RoleList[i].Id,
                    text: _this.RoleList[i].Name
                }
                _this.Options.push(role);
            }
            _this.RoleId = _this.Options[0].value;
        }
    }

    get VisibleStopAuthority() {
        return Boolean(this.UserName && this.Password == this.ConfirmPassword);
    }

    SetCreateUser() {
        const _this = this;
        _this.$bvModal.show('AuthorityModal');
        _this.SaveForm = 'Loading';

        const {
            UserName,
            Password,
            ConfirmPassword,
            RoleId
        } = this;

        const model: RegisterViewModel = {
            UserName: UserName,
            Password: Password,
            ConfirmPassword: ConfirmPassword,
            RoleId: RoleId
        }
        _this.CreateUser(model);
    }

    CreateUser(data) {
        const _this = this;
        console.log(data);
        service.CreateUser(data).then(res => {
            if (!res.Success) {
                _this.SaveForm = 'Error';
                _this.Message = res.Message;
                console.log(res);
            }
            if (res.Success) {
                _this.Message = res.Message;
                _this.SaveForm = 'Success';

            }
        }).catch(err => {
            _this.SaveForm = 'Error';
            console.log(err);
        })
    }

    HideModal() {
        const _this = this;
        _this.$bvModal.hide('AuthorityModal');
    }

    CloseModal() {
        const _this = this;
        _this.$bvModal.hide('AuthorityModal');
        const locationURL = this.httpURL.split("/Account")[0];
        document.location.href = locationURL;
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
