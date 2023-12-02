import React, { useContext, useEffect, useState } from 'react'
import { CategoryListContext } from './Methods';
import { DropPlacement, Icon, TreeNode, TreeNodeLeadingIcon, TreeNodeTitle } from '@kentico/xperience-admin-components';
import CategoryListItemNode from './CategoryListItemNode';
import CategoryItem, { ICategoryItem } from '../../Shared/CategoryItem';

const CategoryListItem =  ({ category, level }: { category: CategoryItem, level: number }) => {
    const { dialogOptions, setDialog, setCategories, categories, setCategory } = useContext(CategoryListContext);

    const [expanded, setExpended] = useState((category.children?.length ?? 0) > 0);

    useEffect(() => {
        !expanded && (category.children?.length ?? 0) > 0 && setExpended(true);
    }, [category.children?.length]);

    const handleDragDrop = (draggedItemID: string, targetItemID: string, dropPlacement: DropPlacement) => {
        // Get Dragged Item
        var category = categories.find(x => ('' + x.categoryID) == draggedItemID);

        // Get target item
        var targetCategory = categories.find(x => ('' + x.categoryID) == targetItemID);

        // Check if going to be a child of target currently not working, but will once fixed
        if (dropPlacement == DropPlacement.Child) {
            if (category && targetCategory) {
                category.categoryParentID = targetCategory.categoryID;
                setCategory(category);
            }
        }
        // Otherwise reorder accordingly.
        else if (targetCategory)
        {
            var target = targetCategory as ICategoryItem;

            var siblings = categories
                .filter(x => x.categoryParentID == target.categoryParentID && x.categoryID != category?.categoryID)
                .sort(((a, b) => (a.categoryOrder ?? 0) - (b.categoryOrder ?? 0)));

            var index = siblings.findIndex(x => x.categoryID == target.categoryID);

            if (dropPlacement == DropPlacement.Below) {
                index++;
            }

            if (category && targetCategory) {
                category.categoryParentID = targetCategory.categoryParentID;
                var newCategories = [
                    ...siblings.slice(0, index)
                    , category
                    , ...siblings.slice(index)
                ]

                setCategories(newCategories);
            }
        }
    }

    return <TreeNode
        name={category.categoryName ?? ""}
        hasChildren={(category.children?.length ?? 0) > 0}
        dropHandler={handleDragDrop}
        nodeIdentifier={category.categoryID?.toString() ?? ""}
        isDraggable={true}
        isToggleable={(category.children?.length ?? 0) > 0}
        onNodeToggle={(e) => setExpended(e)}
        isExpanded={expanded}
        renderNode={(selected, hovered, dragSource) => dragSource && dragSource(<div><CategoryListItemNode selected={selected} category={category} hovered={hovered} /></div>, {})}
        level={level}>
        {category.children && category.children.map(x => <CategoryListItem key={x.categoryID} category={x} level={level + 1} />)}
    </TreeNode>
}

export default CategoryListItem;