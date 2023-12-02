import { usePageCommand } from '@kentico/xperience-admin-base';
import { Headline, HeadlineSize, Icon,TreeNode, TreeNodeLeadingIcon, TreeNodeTitle, TreeNodeTrailingIcon, TreeView, TreeViewContext} from '@kentico/xperience-admin-components';
import React, { useContext, useMemo, useReducer } from 'react'
import './CategoryListPage.css'
import { CategoryListContext, CategoryListDialogOptions, CategoryListPageTemplateProperties, CategoryListReducer, dialogDefaults } from './Components/Methods';
import EditDialog from './Components/EditDialog';
import CategoryListItem from './Components/CategoryListItem';
import CategoryItem, { ICategoryItem } from '../Shared/CategoryItem';
import HierarchicalCategories from '../Shared/HierarchicalCategories';


export const CategoryListPageTemplate = ({ categories }: CategoryListPageTemplateProperties) => {
    const [state, dispatch] = useReducer(CategoryListReducer, {
        categories,
        dialogOptions: dialogDefaults
    });

    const { execute: setCategoryCommand } = usePageCommand<ICategoryItem, ICategoryItem | null>("SetCategory", {
        after: (response) => {
            if (response) {
                dispatch({ type: "categorySet", data: response });
            }
        }
    });

    const { execute: setCategoriesCommand } = usePageCommand<ICategoryItem[], ICategoryItem[] | null>("SetCategories", {
        after: (response) => {
            if (response) {
                dispatch({ type: "categoriesSet", data: response });
            }
        }
    });

    const { execute: deleteCategoryCommand } = usePageCommand<number, number | undefined>("DeleteCategory", {
        after: (response) => {
            if (response) {
                dispatch({ type: "categoryDelete", data: response });
            }
        }
    });

    const hierarchicalList = useMemo(() => {
        return HierarchicalCategories(state.categories);
    }, [state.categories]);

    const setCurrentCategory = (value?: CategoryItem | null) => {
        dispatch({ type: "categorySelect", data: value });
    }

    const setDialog = (dialogOptions: CategoryListDialogOptions) => {
        dispatch({ type: "setDialog", data: dialogOptions });
    }

    const setCategory = (item: CategoryItem) => {
        if (!item.categoryID) {
            var siblings = state.categories.filter(x => x.categoryParentID == item.categoryParentID);
            var order = Math.max(...categories.map(x => x.categoryOrder ?? 0), 0);
            item.categoryOrder = order + 1;
        } else {
            dispatch({ type: "categorySet", data: item })
        }
        setCategoryCommand(item);
    }

    const setCategories = (items: ICategoryItem[]) => {
        var sortedItems = items.map((x, i) => { x.categoryOrder = i; return x });
        dispatch({ type: "categoriesSet", data: items })
        setCategoriesCommand(sortedItems);
    }

    const deleteCategory = (id: number) => {
        deleteCategoryCommand(id);
    }

    return <div>
        <Headline size={HeadlineSize.M}>Category List</Headline>

        <CategoryListContext.Provider value={{ setCategory, setCategories, deleteCategory, setCurrentCategory, dialogOptions: state.dialogOptions, setDialog, categories: state.categories }}>
            <TreeView>        
                <TreeNode name="" hasChildren={true} nodeIdentifier="" isDraggable={true} dropHandler={() => { }} isToggleable={false} isExpanded={true} renderNode={(x, hovered) => <>
                    <TreeNodeLeadingIcon isSelected={x} draggable={false}>
                            <Icon name="xp-parent-child-scheme" />
                    </TreeNodeLeadingIcon>
                    <TreeNodeTitle isSelected={x}>Root Category</TreeNodeTitle>
                    {hovered && <TreeNodeTrailingIcon>
                        <span className="icon-clickable add" style={{ marginRight: 5}} onClick={(e) => { e.stopPropagation(); setDialog({ ...state.dialogOptions, currentCategory: { categoryDisplayName: "" }, isOpen: true, type: "setCategory", headline: "Add Category", message: "" }) }}>
                            <Icon name={"xp-plus-circle"} />
                        </span>
                    </TreeNodeTrailingIcon>}
                    </>} level={0}>
                    {hierarchicalList.map(x => <CategoryListItem key={x.categoryID} category={x} level={1} />)}
                </TreeNode>
            </TreeView>
            <EditDialog />
        </CategoryListContext.Provider>
    </div>
}
 


