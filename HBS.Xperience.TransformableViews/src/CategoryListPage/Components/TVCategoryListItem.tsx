import React, { useContext, useEffect, useState } from 'react'
import { TVCategoryListContext } from './Methods';
import { DropPlacement, Icon, TreeNode, TreeNodeLeadingIcon, TreeNodeTitle } from '@kentico/xperience-admin-components';
import CategoryListItemNode from './TVCategoryListItemNode';
import TransformableViewCategoryItem, { ITransformableViewCategoryItem } from '../../Shared/TransformableViewCategoryItem';

const CategoryListItem =  ({ category, level }: { category: TransformableViewCategoryItem, level: number }) => {
    const { dialogOptions, setDialog, setCategories, categories, setCategory, setCurrentCategory } = useContext(TVCategoryListContext);

    const [expanded, setExpended] = useState((category.children?.length ?? 0) > 0);

    useEffect(() => {
        !expanded && (category.children?.length ?? 0) > 0 && setExpended(true);
    }, [category.children?.length]);

    const handleDragDrop = (draggedItemID: string, targetItemID: string, dropPlacement: DropPlacement) => {
        // Get Dragged Item
        var category = categories.find(x => ('' + x.transformableViewCategoryID) == draggedItemID);

        // Get target item
        var targetCategory = categories.find(x => ('' + x.transformableViewCategoryID) == targetItemID);

        // Check if going to be a child of target currently not working, but will once fixed
        if (dropPlacement == DropPlacement.Child) {
            if (category && targetCategory) {
                category.transformableViewCategoryParentID = targetCategory.transformableViewCategoryID;
                setCategory(category);
            }
        }
        // Otherwise reorder accordingly.
        else if (targetCategory)
        {
            var target = targetCategory as ITransformableViewCategoryItem;

            var siblings = categories
                .filter(x => x.transformableViewCategoryParentID == target.transformableViewCategoryParentID && x.transformableViewCategoryID != category?.transformableViewCategoryID)
                .sort(((a, b) => (a.transformableViewCategoryOrder ?? 0) - (b.transformableViewCategoryOrder ?? 0)));

            var index = siblings.findIndex(x => x.transformableViewCategoryID == target.transformableViewCategoryID);

            if (dropPlacement == DropPlacement.Below) {
                index++;
            }

            if (category && targetCategory) {
                category.transformableViewCategoryParentID = targetCategory.transformableViewCategoryParentID;
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
        name={category.transformableViewCategoryName ?? ""}
        hasChildren={(category.children?.length ?? 0) > 0}
        dropHandler={handleDragDrop}
        nodeIdentifier={category.transformableViewCategoryID?.toString() ?? ""}
        isDraggable={true}
        isToggleable={(category.children?.length ?? 0) > 0}
        onNodeToggle={(e) => setExpended(e)}
        isExpanded={expanded}
        onNodeClick={() => { setCurrentCategory(category) } }
        renderNode={(selected, hovered, dragSource) => dragSource && dragSource(<div><CategoryListItemNode selected={selected} category={category} hovered={hovered} /></div>, {})}
        level={level}>
        {category.children && category.children.map(x => <CategoryListItem key={x.transformableViewCategoryID} category={x} level={level + 1} />)}
    </TreeNode>
}

export default CategoryListItem;