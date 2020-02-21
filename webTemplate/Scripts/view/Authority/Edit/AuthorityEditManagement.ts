import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import { AuthorityViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');
import service from '../service';

@Component({
    template: '#AuthorityEditManagement'
})

export default class AuthorityEditManagement extends Vue {

    httpURL: string = window.location.href;

    AspNetUserId: string = '';

    UserName: string = '';
    Password: string = '';
    ConfirmPassword: string = '';

    PasswordTypes: string = "password";
    ShowPassword: boolean = false;

    Select: string = '';

    RoleList: RoleList[] = [];

    Options: RoleOptions[] = [];

    ListItem: AuthorityViewModel | null = null;

    created() {
        const _this = this;
        _this.GetRoleOptions();
        _this.AspNetUserId = _this.httpURL.split("?Id=")[1];
        _this.GetAuthorityItem(_this.AspNetUserId);
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

    GetAuthorityItem(Id) {
        const _this = this;
        service.GetEditAuthorityItem(Id).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.ListItem = res.Data;
                _this.UserName = res.Data.UserName;
                _this.Select = res.Data.RoleId;
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
            _this.Select = _this.Options[0].value;
        }
    }

    EditAuthorityItem() {
        const _this = this;
        if (_this.ListItem) {
            _this.ListItem.UserName = _this.UserName;
            _this.ListItem.Password = _this.Password;
            _this.ListItem.RoleId = _this.Select;
            service.EditAuthorityItem(_this.ListItem).then(res => {
                if (!res.Success) {
                    console.log(res);
                } if (res.Success) {
                    const locationURL = this.httpURL.split("/Edit?")[0];
                    document.location.href = locationURL;
                }
            }).catch(err => {
                console.log(err);
            })
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