import React, { useContext } from 'react'
import { CategoryListContext } from './Methods';
import { Icon, TreeNodeLeadingIcon, TreeNodeTitle, TreeNodeTrailingIcon } from '@kentico/xperience-admin-components';
import CategoryItem from '../../Shared/CategoryItem';

export default ({ selected, category, hovered }: { selected: boolean, category: CategoryItem, hovered: boolean }) => {
    const { setDialog, dialogOptions } = useContext(CategoryListContext);

    const addNew = (currentCategory: CategoryItem) => {
        setDialog({ ...dialogOptions, currentCategory, isOpen: true, type: "setCategory", headline: "Add Category", message: "Add category to parent category: " + category.categoryDisplayName })
    }

    const updateCategory = () => {
        setDialog({ ...dialogOptions, currentCategory: category, isOpen: true, type: "setCategory", headline: "Update Category", message: "" })
    }

    const deleteCategory = () => {
        setDialog({ ...dialogOptions, currentCategory: category, isOpen: true, type: "deleteCategory", headline: "Delete Category", message: "Delete category: " + category.categoryDisplayName })
    }
    return <div style={{display: 'flex', flexDirection: 'row', alignItems: 'center'}}>
         <TreeNodeLeadingIcon isSelected={selected} draggable={true}>
            <Icon name={hovered ? "xp-dots-vertical" : "xp-parent-child-scheme"} />
        </TreeNodeLeadingIcon>
        <TreeNodeTitle isSelected={selected}>{category.categoryDisplayName}</TreeNodeTitle>
        {hovered && <><TreeNodeTrailingIcon>
            <span className="icon-clickable" onClick={(e) => { e.stopPropagation(); updateCategory() }}>
                <Icon name="xp-edit" />
            </span>
        </TreeNodeTrailingIcon>
            <TreeNodeTrailingIcon>
                <span className="icon-clickable add" onClick={(e) => { e.stopPropagation(); addNew({ categoryDisplayName: "", categoryParentID: category.categoryID }) }}>
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