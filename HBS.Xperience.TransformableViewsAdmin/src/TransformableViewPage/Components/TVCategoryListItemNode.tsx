import React, { useContext } from 'react'
import { TVCategoryListContext } from './Methods';
import { Icon, TreeNodeLeadingIcon, TreeNodeTitle, TreeNodeTrailingIcon } from '@kentico/xperience-admin-components';
import TransformableViewCategoryItem from '../../Shared/TransformableViewCategoryItem';

export default ({ selected, category, hovered }: { selected: boolean, category: TransformableViewCategoryItem, hovered: boolean }) => {
    const { setDialog, dialogOptions  } = useContext(TVCategoryListContext);

    const addNew = (currentCategory: TransformableViewCategoryItem) => {
        setDialog({ dialogOptions: { ...dialogOptions, isOpen: true, type: "setCategory", headline: "Add Category", message: "Add category to parent category: " + category.transformableViewCategoryDisplayName }, selectCategory: currentCategory })
    }

    const updateCategory = () => {
        setDialog({ dialogOptions: { ...dialogOptions, isOpen: true, type: "setCategory", headline: "Update Category", message: "" }, selectCategory: category })
    }

    const deleteCategory = () => {
        setDialog({ dialogOptions: { ...dialogOptions, isOpen: true, type: "deleteCategory", headline: "Delete Category", message: "Delete category: " + category.transformableViewCategoryDisplayName }, selectCategory: category })
    }
    return <div style={{display: 'flex', flexDirection: 'row', alignItems: 'center'}}>
         <TreeNodeLeadingIcon isSelected={selected} draggable={true}>
            <Icon name={hovered ? "xp-dots-vertical" : "xp-parent-child-scheme"} />
        </TreeNodeLeadingIcon>
        <TreeNodeTitle isSelected={selected}>{category.transformableViewCategoryDisplayName}</TreeNodeTitle>
        {hovered && <><TreeNodeTrailingIcon>
            <span className="icon-clickable" onClick={(e) => { e.stopPropagation(); updateCategory() }}>
                <Icon name="xp-edit" />
            </span>
        </TreeNodeTrailingIcon>
            <TreeNodeTrailingIcon>
                <span className="icon-clickable add" onClick={(e) => { e.stopPropagation(); addNew({ transformableViewCategoryDisplayName: "", transformableViewCategoryParentID: category.transformableViewCategoryID }) }}>
                    <Icon name="xp-plus-circle" />
                </span>
            </TreeNodeTrailingIcon>
            <TreeNodeTrailingIcon>
                <span className="icon-clickable delete" onClick={(e) => { e.stopPropagation(); deleteCategory() }}>
                    <Icon name="xp-minus-circle" />
                </span>
            </TreeNodeTrailingIcon>
        </>}
    </div>
}