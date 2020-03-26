import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import service from './service';
import { StaticPageViewModel } from './model'
import moment = require('moment');
import VueEditor, { Quill } from 'vue2-editor';
import ImageResize from 'quill-image-resize-module';

Quill.register('modules/imageResize', ImageResize);

Vue.use(VueEditor);

var icons = Quill.import('ui/icons');
icons['html'] = '<i class="fas fa-code"></i>'

@Component({
    template: '#StaticPageItem'
})

export default class StaticPageItem extends Vue {
    @Prop(Object) ListItem: StaticPageViewModel | undefined;

    PageContent: string = '';
    StaticPageItem: StaticPageViewModel | null = null;
    editorOption: object = {};
    htmlFromEditor: boolean = true;
    customToolbar = [[{
        header: [false, 1, 2, 3, 4, 5, 6]
    }], ["bold", "italic", "underline", "strike"], // toggled buttons
    [{
        align: ""
    }, {
        align: "center"
    }, {
        align: "right"
    }, {
        align: "justify"
    }], ["blockquote"], [{
        list: "ordered"
    }, {
        list: "bullet"
    }, {
        list: "check"
    }], [{
        indent: "-1"
    }, {
        indent: "+1"
    }], // outdent/indent
    [{
        color: []
    }, {
        background: []
    }], // dropdown with defaults from theme
    ["link", "image", "video"], ["clean"], ["html"] // remove formatting button
    ];

    created() {
        const _this = this;
        _this.SetEditorOption();
    }

    mounted() {
        const _this = this;
        const htmlButton = _this.$el.querySelector('.ql-html');
        if (htmlButton != null) {
            htmlButton.addEventListener('click', function () {
                var htmlEditor = _this.$el.querySelector('.ql-html-editor');
                if (htmlEditor) {
                    const qlEditor = _this.$el.querySelector(".ql-editor");
                    if (qlEditor) {
                        let qlHTMLEditor = $('.ql-html-editor').val();
                        if (qlHTMLEditor) {
                            let qlChange = qlHTMLEditor.toString();
                            qlHTMLEditor = qlChange.replace(/\n/g, "");
                            qlEditor.innerHTML = qlHTMLEditor;
                        }
                        const qlContainerEditor = _this.$el.querySelector(".ql-container");
                        if (qlContainerEditor) {
                            qlContainerEditor.removeChild(htmlEditor);
                        }
                    }
                } else {
                    htmlEditor = document.createElement("textarea");
                    htmlEditor.className = 'ql-editor ql-html-editor';
                    const qlEditor = _this.$el.querySelector(".ql-editor");
                    if (qlEditor) {
                        const qlHTMLEditor = qlEditor.innerHTML;
                        htmlEditor.innerHTML = qlHTMLEditor.replace(/\n\n/g, "\n");
                        const qlContainerEditor = _this.$el.querySelector(".ql-container");
                        if (qlContainerEditor) {
                            qlContainerEditor.appendChild(htmlEditor);
                        }
                    }
                };
            })
        };

    }

    SetEditorOption() {
        const _this = this;
        _this.editorOption = {
            modules: {
                imageResize: {
                    displaySize: true
                }
            }
        }
    }

    SubmitCompanyProfile() {
        const _this = this;
        _this.SubmitStaticPage(_this.PageContent);
    }

    SubmitStaticPage(data) {
        const _this = this;
        if (_this.ListItem) {
            _this.ListItem.PageContent = data;

            service.EditStaticPage(_this.ListItem).then(res => {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.$bvToast.toast("變更'公司介紹'成功", {
                        title: '變更靜態頁',
                        autoHideDelay: 50,
                        variant: 'success'
                    })
                }
            }).catch(err => {
                console.log(err);
            })
        }
    }

    /**
     * 新增靜態頁內容
     * @param data
     */
    //SubmitService(data) {
    //    const _this = this;
    //    _this.StaticPageItem = {
    //        StaticPageId: 1,
    //        StaticPageEnum: 3,
    //        PageContent: data,
    //        CreateTime: moment().toDate(),
    //        CreateUser: '',
    //        UpdateTime: moment().toDate(),
    //        UpdateUser: ''
    //    }
    //    service.CreateStaticPage(_this.StaticPageItem).then(res => {
    //        if (!res.Success) {
    //            console.log(res);
    //        }
    //        if (res.Data) {
    //            _this.StaticPageItem = res.Data
    //        }
    //    }).catch(err => {
    //        console.log(err);
    //    })
    //}

    @Watch('ListItem')
    OnListItemChange() {
        const _this = this;
        if (_this.ListItem) {
            _this.PageContent = _this.ListItem.PageContent;
        }
    }

}