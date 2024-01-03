import TransformableViewCategoryItem from "./TransformableViewCategoryItem";

export default (categories: TransformableViewCategoryItem[]) => {
    categories.forEach(f => {
        f.children = categories
            .filter(g => g.transformableViewCategoryParentID === f.transformableViewCategoryID)
            .sort((a, b) => (a.transformableViewCategoryOrder ?? 0) - (b.transformableViewCategoryOrder ?? 0))
    });
    var resultList = categories.filter(f => !f.transformableViewCategoryParentID).sort((a, b) => (a.transformableViewCategoryOrder ?? 0) - (b.transformableViewCategoryOrder ?? 0));
    return resultList;
}