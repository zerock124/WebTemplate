import { Vue, Component, Prop } from 'vue-property-decorator'
import { ContactViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import service from '../service'
import moment = require('moment');
import VueEditor from 'vue2-editor'

Vue.use(VueEditor);

@Component({
    template: '#ContactEditManagement'
})

export default class ContactEditManagement extends Vue {

    httpURL: string = window.location.href;

    ContactItem: ContactViewModel | null = null;

    ContactId: number = 0;
    SexOptions: object[] = [
        { value: 0, text: '先生' },
        { value: 1, text: '小姐' }
    ]
    EnumOptions: object[] = [
        { value: 0, text: '網站建置' },
        { value: 1, text: '聊天機器人' },
        { value: 2, text: '客製化抽獎' },
        { value: 3, text: '其它' }
    ]
    StatusOptions: object[] = [
        { value: 0, text: '全新專案' },
        { value: 1, text: '改版增修' }
    ]
    customToolbar = [
        ["bold", "italic", "underline"],
        [{ list: "ordered" },
        { list: "bullet" }]
    ]

    SaveForm: string = 'Loading';

    created() {
        const _this = this;
        _this.GetContactId();
        _this.GetContactItem();
    }

    GetContactId() {
        const _this = this;
        let httpURL = window.location.href;
        _this.ContactId = parseInt(httpURL.split("?ContactId=")[1]);
    }

    GetContactItem() {
        const _this = this;
        service.GetEditContactItem(_this.ContactId).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.ContactItem = res.Data;
            }
        }).catch(err => {
            console.log(err);
        })
    };

    SetEditFontHome() {
        const _this = this;
        _this.$bvModal.show('ContactModal');
        _this.SaveForm = 'Loading';
        if (_this.ContactItem) {
            service.EditContactItem(_this.ContactItem).then(res => {
                if (!res.Success) {
                    _this.SaveForm = 'Error';
                    console.log(res);
                }
                if (res.Success) {
                    _this.SaveForm = 'Success';

                }
            }).catch(err => {
                _this.SaveForm = 'Error';
                console.log(err);
            })
        }
    }

    HideModal() {
        const _this = this;
        _this.$bvModal.hide('ContactModal');
    }

    CloseModal() {
        const _this = this;
        _this.$bvModal.hide('ContactModal');
        const locationURL = this.httpURL.split("/Edit?")[0];
        document.location.href = locationURL;
    }

}