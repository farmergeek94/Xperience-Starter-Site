import React, { useContext, useEffect, useState } from 'react'
import { CodeEditor, CodeEditorLanguage, Dialog, Input, MenuItem, RadioButton, RadioGroup, Select, TextWithLabel } from '@kentico/xperience-admin-components';

export interface InputTypeDialogOptions {
    open: boolean
    setInputType: (type: string, name: string) => void
    closeDialog: () => void
}

export default ({ setInputType, open, closeDialog }: InputTypeDialogOptions) => {
    const [type, setType] = useState("text");
    const [name, setName] = useState("");

    const handleCancel = () => {
        closeDialog();
    }

    const handleConfirm = () => {
        setInputType(type, name);
        closeDialog();
    }

    return <Dialog isOpen={open} headline={"Select Type"} overlayClassName="dialog-z-index" onClose={handleCancel} isDismissable={true} headerCloseButton={{ tooltipText: "Close Dialog" }} confirmAction={{ label: "Okay", onClick: handleConfirm }} cancelAction={{ label: "Cancel", onClick: handleCancel }}>
        <div style={{ paddingBottom: 15 }} onClick={(e) => e.stopPropagation()} onChange={(e) => e.stopPropagation()}>
            <RadioGroup name="Type" label='Type' value={ type } onChange={(val) => setType(val ?? "text")}>
                <RadioButton value='text'> Text </RadioButton>
                <RadioButton value='number'> Number </RadioButton>
                <RadioButton value='email'> Email </RadioButton>
            </RadioGroup>
        </div>
        <div style={{ paddingBottom: 15 }}>
            <Input label="Name" value={name} onChange={(e) => setName(e.currentTarget.value)} />
        </div>
    </Dialog>
}