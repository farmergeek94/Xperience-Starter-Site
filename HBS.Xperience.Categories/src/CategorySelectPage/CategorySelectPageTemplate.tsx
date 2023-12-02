import React, { createContext, useContext, useMemo, useReducer, useState } from 'react'
import CategoryItem from '../Shared/CategoryItem'
import { Headline, HeadlineSize, Icon, TreeNode, TreeNodeLeadingIcon, TreeNodeTitle, TreeNodeTrailingIcon, TreeView } from '@kentico/xperience-admin-components'
import { usePageCommand } from '@kentico/xperience-admin-base'
import HierarchicalCategories from '../Shared/HierarchicalCategories'

interface ContentItemCategoryItem {
    categoryID: number
    contentItemID: number
}

interface CategorySelectPageProperties {
    categories: CategoryItem[]
    contentItemCategories?: ContentItemCategoryItem[]
    contentItemID?: number | null
}

interface CategorySelectPageState {
    contentItemCategoryItems?: ContentItemCategoryItem[]
    contentItemID?: number | null
}

type CategoryActions = { type: "add-category", data: number } | { type: "remove-category", data: number }

const CategoryListReducer = (state: CategorySelectPageState, action: CategoryActions) => {
    const { type, data } = action;
    const newState = { ...state };
    switch (type) {
        case "add-category":
            newState.contentItemCategoryItems?.push({ categoryID: data, contentItemID: newState.contentItemID ?? -1 });
            if (newState.contentItemCategoryItems) {
                newState.contentItemCategoryItems = [...newState.contentItemCategoryItems];
            }
            break;
        case "remove-category":
            newState.contentItemCategoryItems = newState.contentItemCategoryItems?.filter(x=>x.categoryID != data);
            break;
    }
    return newState;
}

interface ICategorySelectPageContext {
    categories: CategoryItem[],
    contentItemCategoryItems?: ContentItemCategoryItem[]
    addCategory?: (x: number) => void,
    removeCategory?: (x: number) => void
}

const CategorySelectPageContext = createContext<ICategorySelectPageContext>({ categories: [] });


export const CategorySelectPageTemplate = ({ categories, contentItemCategories, contentItemID }: CategorySelectPageProperties) => {

    const [state, dispatch] = useReducer(CategoryListReducer, { contentItemCategoryItems: contentItemCategories, contentItemID });

    const { execute: addCategory } = usePageCommand<number, number>("AddContentItemCategory", {
        after: (response) => {
            if (response) {
                dispatch({ type: "add-category", data: response });
            }
        }
    });

    const { execute: removeCategory } = usePageCommand<number, number>("RemoveContentItemCategory", {
        after: (response) => {
            if (response) {
                dispatch({ type: "remove-category", data: response });
            }
        }
    });

    const hierarchicalList = useMemo(() => {
        return HierarchicalCategories(categories);
    }, [categories]);


    return <div style={{ padding: 15 }}>
        <Headline size={HeadlineSize.M}>Categories</Headline>
        <div>
        <CategorySelectPageContext.Provider value={{ categories, addCategory, removeCategory, contentItemCategoryItems: state.contentItemCategoryItems }}>
            <TreeView>
                    {hierarchicalList.map(x => <CategoryListItem key={x.categoryID} category={x} level={1} />)}
            </TreeView>
            </CategorySelectPageContext.Provider>
        </div>
    </div>
}

// Export the class needed for the content item template
export const CategorySelectContentItemTemplate = CategorySelectPageTemplate;

const CategoryListItem = ({ category, level }: { category: CategoryItem, level: number }) => {
    var hasChildren = () => (category.children?.length ?? 0) > 0;
    const [expand, setExpand] = useState(hasChildren());

    const { contentItemCategoryItems, addCategory, removeCategory } = useContext(CategorySelectPageContext);

    const isSelected = useMemo(() => {
        return contentItemCategoryItems?.find(x => x.categoryID == category.categoryID);
    }, [category, contentItemCategoryItems]);

    const toggleCategory = () => {

        if (isSelected) {
            removeCategory && category.categoryID && removeCategory(category.categoryID);
        } else {
            addCategory && category.categoryID && addCategory(category.categoryID);
        }
    }

    return <TreeNode
        name={category.categoryName + ''}
        hasChildren={hasChildren()}
        nodeIdentifier={'' + category.categoryID}
        isDraggable={true}
        isToggleable={hasChildren()}
        onNodeToggle={(e) => setExpand(e)}
        isSelectable={true}
        isExpanded={expand}
        dropHandler={() => { }}
        renderNode={(x, hovered) => <>
            <TreeNodeLeadingIcon isSelected={x} draggable={false}>
                <Icon name="xp-parent-child-scheme" />
            </TreeNodeLeadingIcon>
            <TreeNodeTitle isSelected={x}>{category.categoryDisplayName}</TreeNodeTitle>
            <TreeNodeTrailingIcon>
                <span style={{ color: isSelected ? "green" : "darkgray", paddingRight: 10 }}>
                    <Icon name={isSelected ? "xp-check-circle" : "xp-circle"} />
                </span>
            </TreeNodeTrailingIcon>
        </>}
        onNodeClick={toggleCategory}
        level={level}>
        {category.children && category.children.map(x => <CategoryListItem key={x.categoryID} category={x} level={level + 1} />)}
    </TreeNode>
}