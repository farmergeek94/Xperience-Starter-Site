import React, { useContext, useEffect, useState } from 'react'
import { CategoryListContext } from './Methods';
import { Dialog, Input } from '@kentico/xperience-admin-components';
export default () => {
    const { setCategory, deleteCategory, dialogOptions, setDialog } = useContext(CategoryListContext);
    const [displayName, setDisplayName] = useState("");
    useEffect(() => { setDisplayName(dialogOptions.currentCategory?.categoryDisplayName ?? "") }, [dialogOptions.currentCategory])
    const handleCancel = () => {
        setDialog({ ...dialogOptions, isOpen: false, currentCategory: null })
    }
    const handleConfirm = () => {
        if (dialogOptions.type == "setCategory" && dialogOptions.currentCategory) {
            setCategory({ ...dialogOptions.currentCategory, categoryDisplayName: displayName });
        } else if (dialogOptions.type == "deleteCategory" && dialogOptions.currentCategory) {
            deleteCategory(dialogOptions.currentCategory.categoryID ?? -1);
        }
        setDialog({ ...dialogOptions, isOpen: false, currentCategory: null })
    }
    return <Dialog isOpen={dialogOptions.isOpen} headline={dialogOptions.headline} onClose={handleCancel} isDismissable={true} headerCloseButton={{ tooltipText: "Close Dialog" }} confirmAction={{ label: "Okay", onClick: handleConfirm }} cancelAction={{ label: "Cancel", onClick: handleCancel }}>
        {dialogOptions.type == "setCategory" && <Input label="Display Name" value={displayName} onChange={ (e) => setDisplayName(e.currentTarget.value) } />}
        {dialogOptions.message}
    </Dialog>
}