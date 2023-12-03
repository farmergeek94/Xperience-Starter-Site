import { Icon, TreeNode, TreeNodeLeadingIcon, TreeNodeTitle, TreeNodeTrailingIcon, TreeView } from '@kentico/xperience-admin-components';
import React, { useContext, useMemo } from 'react'
import CategoryListItem from './TVCategoryListItem';
import { TVCategoryListContext } from './Methods';
import HierarchicalCategories from '../../Shared/HierarchicalCategories';

export default () => {
    const { categories, setDialog, dialogOptions } = useContext(TVCategoryListContext);

    const hierarchicalList = useMemo(() => {
        return HierarchicalCategories(categories);
    }, [categories]);

    return <TreeView>
        <TreeNode name="" hasChildren={true} nodeIdentifier="" isDraggable={true} dropHandler={() => { }} isToggleable={false} isExpanded={true} renderNode={(x, hovered) => <>
            <TreeNodeLeadingIcon isSelected={x} draggable={false}>
                <Icon name="xp-parent-child-scheme" />
            </TreeNodeLeadingIcon>
            <TreeNodeTitle isSelected={x}>Root Category</TreeNodeTitle>
            {hovered && <TreeNodeTrailingIcon>
                <span className="icon-clickable add" style={{ marginRight: 5 }} onClick={(e) => { e.stopPropagation(); setDialog({ dialogOptions: { ...dialogOptions, isOpen: true, type: "setCategory", headline: "Add Category", message: "" }, selectCategory: { transformableViewCategoryDisplayName: "" } }) }}>
                    <Icon name={"xp-plus-circle"} />
                </span>
            </TreeNodeTrailingIcon>}
        </>} level={0}>
            {hierarchicalList.map(x => <CategoryListItem key={x.transformableViewCategoryID} category={x} level={1} />)}
        </TreeNode>
    </TreeView>
}