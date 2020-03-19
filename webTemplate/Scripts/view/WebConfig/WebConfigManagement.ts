import { Vue, Component, Watch, Emit, Prop } from 'vue-property-decorator'
import { IWebConfigService, WebConfigViewModel } from './model'
import service from './service'
import moment = require('moment')

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

    updated() {
        this.setAutocomplete('input-address');
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
            _this.SaveForm = "Success"
        }, 1000);
    }

    CloseModel() {
        const _this = this;
        _this.$bvModal.hide("WebConfigModal");
    }

    setAutocomplete(id: string) {
        let input = document.getElementById(id) as HTMLInputElement;
        let autocomplete = new google.maps.places.Autocomplete(input);

        //------------------------當按下Enter選取第一筆資料-----------------

        //autocomplete.setTypes(['address'])

        enableEnterKey(input)

        function enableEnterKey(input) {
            /* 設定原始Input框的監聽事件 */
            const _addEventListener = input.addEventListener;

            const addEventListenerWrapper = (type, listener) => {
                if (type === "keydown") {
                    /* 設定現在Input框的監聽事件 */
                    const _listener = listener;
                    listener = (event) => {
                        /* 如果沒有選擇地址，則模擬方向鍵"下"按鍵 */
                        const suggestionSelected = document.getElementsByClassName('pac-item-selected').length;
                        if (event.key === 'Enter' && !suggestionSelected) {
                            const e = new $.Event("keydown", { key: "ArrowDown", code: "ArrowDown", keyCode: 40 });

                            _listener.apply(input, [e])
                        }
                        _listener.apply(input, [event])
                    }
                }
                _addEventListener.apply(input, [type, listener])
            }

            input.addEventListener = addEventListenerWrapper;
        }

        //-------------------------------------------------------------

        autocomplete.setFields(
            ['address_components', 'geometry', 'icon', 'name', 'formatted_address']);
        autocomplete.setComponentRestrictions({ country: "tw" });

        ////限制範圍
        //var cityBounds = new google.maps.LatLngBounds();

        //for (var i = 0; i < this.path.length; i++) {
        //    cityBounds.extend(this.path[i]);
        //}

        //console.log(cityBounds);

        ////設定autocomplete執行此限制範圍     
        //autocomplete.setBounds(cityBounds);

        ////如果有限制範圍且要autocomplete也限制的話需要下方code
        //autocomplete.setOptions({ strictBounds: true });
        autocomplete.addListener('place_changed', () => {
            this.place_changed(autocomplete.getPlace());
        });
    }

    //地址改變事件
    place_changed(place: google.maps.places.PlaceResult) {
        console.log(place);
        if (!place.geometry) {
            // User entered the name of a Place that was not suggested and
            // pressed the Enter key, or the Place Details request failed.
            this.$bvToast.toast('「' + place.name + '」並不再範圍內', {
                title: '地點錯誤',
                variant: 'danger',
            })
            return;
        }
        let address = ''
        if (place.address_components) {
            address = [
                (place.address_components[6] && place.address_components[6].short_name || ''),
                (place.address_components[4] && place.address_components[4].short_name || ''),
                (place.address_components[3] && place.address_components[3].short_name || ''),
                (place.address_components[1] && place.address_components[1].short_name || ''),
                (place.address_components[0] && place.address_components[0].short_name || ''),
            ].join(' ');
        }

        if (this.WebConfigItem && place.formatted_address) {
            this.WebConfigItem.CompanyAddress = place.formatted_address;
            this.WebConfigItem.Lat = place.geometry.location.lat();
            this.WebConfigItem.Lng = place.geometry.location.lng();
        }
    }
}
