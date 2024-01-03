import React, { useContext, useEffect, useState } from 'react'
import { CodeEditor, CodeEditorLanguage, Dialog, Input, MenuItem, Select, TextWithLabel } from '@kentico/xperience-admin-components';

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

    return <Dialog isOpen={open} headline={"Select Type"} onClose={handleCancel} isDismissable={true} headerCloseButton={{ tooltipText: "Close Dialog" }} confirmAction={{ label: "Okay", onClick: handleConfirm }} cancelAction={{ label: "Cancel", onClick: handleCancel }}>
        <div style={{ paddingBottom: 15 }}>
           <Select label="Type" value={type} onChange={(e) => { setType(e ?? "text") }}>
                <MenuItem primaryLabel="Text" value="text" />
                <MenuItem primaryLabel="Number" value="number" />
                <MenuItem primaryLabel="Email" value="email" />
            </Select>
        </div>
        <div style={{ paddingBottom: 15 }}>
            <Input label="Name" value={name} onChange={(e) => setName(e.currentTarget.value)} />
        </div>
    </Dialog>
}