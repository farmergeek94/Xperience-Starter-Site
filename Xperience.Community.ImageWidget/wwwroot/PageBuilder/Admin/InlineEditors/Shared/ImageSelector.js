(function () {
    // Registers the 'custom-editor' inline property editor
    window.kentico.pageBuilder.registerInlineEditor("image-widget-image-selector", {
        init: function (options) {
            var editor = options.editor;
            
            window.imageWidet = options;
            var button = editor.querySelector(".image-selector-editor-button");
            button.addEventListener("click", function (e) {
                e.preventDefault();
                kentico.modalDialog.contentSelector.open({
                    tabs: [
                        "media"
                    ],
                    applyCallback: function (data) {
                        console.log(data);
                        // Update the property.
                        var event = new CustomEvent("updateProperty", {
                            detail: {
                                value: data.items.map(function (x) { return { identifier: x.fileGuid } }),
                                name: options.propertyName
                            }
                        });
                        editor.dispatchEvent(event);
                    },
                    selectedItems: {
                        type: "media"
                        , items: options.propertyValue.map(function (x) { return { value: x.identifier } })
                    },
                    selectedItemsLimit: 1
                })
            });
        }
    });
}) ();

