//添加easyui的自定义方法实现form load的问题
$.extend($.fn.form.methods, {
    myload: function (jq, data) {
        return jq.each(
            function () {
                loadVal(this, data);
            }
        );

        function loadVal(formElement, data) {
            if (!$.data(formElement, "form")) {
                $.data(formElement, "form", {
                    options: $.extend({}, $.fn.form.defaults)
                });
            }
            var opts = $.data(formElement, "form").options;
            if (typeof data == "string") {
                var optSource = {};
                if (opts.onBeforeLoad.call(formElement, optSource) == false) {
                    return;
                }
                $.ajax({
                    url: data,
                    data: optSource,
                    dataType: "json",
                    success: function (data) {
                        bindValue(data);
                    },
                    error: function () {
                        opts.onLoadError.apply(formElement, arguments);
                    }
                });
            } else {
                bindValue(data);
            }

            function bindValue(data) {
                var form = $(formElement);
                for (var name in data) {
                    var val = data[name];
                    var rr = setRadioAndCheckBox(name, val);
                    if (!rr.length) {
                        var f = form.find("input[numberboxName=\"" + name + "\"]");
                        if (f.length) {
                            f.numberbox("setValue", val);
                        } else {
                            if (typeof val === 'object' && val != null) {
                                $.each(val, function (cName, value) {

                                    var crr = setRadioAndCheckBox(name + "." + cName, value);
                                    if (!crr.length) {
                                        var cf = form.find("input[numberboxName=\"" + name + "." + cName + "\"]");
                                        if (cf.length) {
                                            cf.numberbox("setValue", value);
                                        } else {
                                            $("input[name=\"" + name + "." + cName + "\"]", form).val(value);
                                            $("textarea[name=\"" + name + "." + cName + "\"]", form).val(value);
                                            $("select[name=\"" + name + "." + cName + "\"]", form).val(value);
                                        }
                                    }
                                });
                            } else {
                                $("input[name=\"" + name + "\"]", form).val(val);
                                $("textarea[name=\"" + name + "\"]", form).val(val);
                                $("select[name=\"" + name + "\"]", form).val(val);
                            }
                        }
                    }
                    setEasyUiCtrl(name, val);
                }
                opts.onLoadSuccess.call(formElement, data);
                setValBox(formElement);
            }

            ;

            function setRadioAndCheckBox(name, val) {
                var rr = $(formElement).find("input[name=\"" + name + "\"][type=radio], input[name=\"" + name + "\"][type=checkbox]");
                rr._propAttr("checked", false);
                rr.each(function () {
                    var f = $(this);
                    //避免字符串大小写问题
                    if (f.val().toUpperCase() == String(val).toUpperCase() || $.inArray(f.val(), val) >= 0) {
                        f._propAttr("checked", true);
                    }
                });
                return rr;
            }

            ;

            function setEasyUiCtrl(name, val) {
                var form = $(formElement);
                var cc = ["combobox", "combotree", "combogrid", "datetimebox", "datebox", "combo"];
                var c = form.find("[comboName=\"" + name + "\"]");
                if (c.length) {
                    for (var i = 0; i < cc.length; i++) {
                        var type = cc[i];
                        if (c.hasClass(type + "-f")) {
                            if (c[type]("options").multiple) {
                                c[type]("setValues", val);
                            } else {
                                c[type]("setValue", val);
                            }
                            return;
                        }
                    }
                }
            }

            ;

            function setValBox(eleForm) {
                if ($.fn.validatebox) {
                    var t = $(eleForm);
                    t.find(".validatebox-text:not(:disabled)").validatebox("validate");
                    var valBox = t.find(".validatebox-invalid");
                    valBox.filter(":not(:disabled):first").focus();
                    return valBox.length == 0;
                }
                return true;
            }

            ;
        }
    }
});