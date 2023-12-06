import React, { useContext, useEffect, useState } from 'react'
import { CodeEditor, CodeEditorLanguage, Dialog, Input, TextWithLabel } from '@kentico/xperience-admin-components';
import TransformableViewItem from '../../Shared/TransformableViewItem';

export interface ViewDialogOptions {
    selectedView?: TransformableViewItem
    open: boolean
    setViewsCommand: (view: TransformableViewItem) => void
    closeDialog: () => void
}

export default ({ selectedView, setViewsCommand, open, closeDialog }: ViewDialogOptions) => {
    const [displayName, setDisplayName] = useState(selectedView?.transformableViewDisplayName);
    const [content, setContent] = useState(selectedView?.transformableViewContent);

    const handleCancel = () => {
        closeDialog();
    }

    const handleConfirm = () => {
        selectedView && setViewsCommand({ ...selectedView, transformableViewDisplayName: displayName ?? "", transformableViewContent: content ?? "" });
        closeDialog();
    }

    return <Dialog isOpen={open} headline={"Edit View"} onClose={handleCancel} isDismissable={true} headerCloseButton={{ tooltipText: "Close Dialog" }} confirmAction={{ label: "Okay", onClick: handleConfirm }} cancelAction={{ label: "Cancel", onClick: handleCancel }}>
        <div style={{paddingBottom: 20}}>
            <Input label="Display Name" value={displayName} onChange={(e) => setDisplayName(e.currentTarget.value)} />
        </div>
        <div style={{ paddingBottom: 20 }}>
            <Input label="Code Name" value={selectedView?.transformableViewName} disabled={ true } />
        </div>
        <div>
            <TextWithLabel label="View Editor" value={ `<div><span style="color: #af00db;">@addTagHelper</span> <span style="color: #a31515;">*, Microsoft.AspNetCore.Mvc.TagHelpers</span></div><div><span style="color: #af00db;">@model</span> <span style="color: #267f99;">HBS</span><span style="color: #000000;">.</span><span style="color: #267f99;">Xperience</span><span style="color: #000000;">.</span><span style="color: #267f99;">TransformableViews</span><span style="color: #000000;">.</span><span style="color: #267f99;">Models</span><span style="color: #000000;">.</span><span style="color: #267f99;">TransformableViewModel</span></div>` } valueAsHtml={true} />
            <CodeEditor style={{ maxWidth: "100%", width: "1000px" }} language={'html'} value={content} onChange={(e) => setContent(e)} explanationText={`
                TransformableViewModel contains the following properties:  <span style="color: #0000ff;">string</span> <span style="color: #001080;">ViewTitle</span>,  <span style="color: #0000ff;">string</span> <span style="color: #001080;">ViewClassNames</span>,  <span style="color: #0000ff;">string</span> <span style="color: #001080;">ViewCustomContent</span>,  <span style="color: #0000ff;"><span style="color: #0000ff;">IEnumerable&lt;</span></span><span style="color: #800000;">dynamic</span><span style="color: #0000ff;">&gt;</span> <span style="color: #001080;">Items</span>.<br/>  You can reference them like a normal view using <span style="color: #af00db;">@</span><span style="color: #001080;">Model</span><span style="color: #000000;">.</span><span style="color: #001080;">Items</span><br/>To reference other transformable views, append <b>TransformableView/</b> to the view's code name e.g. <b>TransformableView/TestPartialView</b>.
            `} explanationTextAsHtml={ true } />
        </div>
    </Dialog>
}