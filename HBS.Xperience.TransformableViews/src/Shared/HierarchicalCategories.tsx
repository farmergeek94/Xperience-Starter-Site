import CategoryItem from "./CategoryItem";

export default (categories: CategoryItem[]) => {
    categories.forEach(f => {
        f.children = categories
            .filter(g => g.categoryParentID === f.categoryID)
            .sort((a, b) => (a.categoryOrder ?? 0) - (b.categoryOrder ?? 0))
    });
    var resultList = categories.filter(f => !f.categoryParentID).sort((a, b) => (a.categoryOrder ?? 0) - (b.categoryOrder ?? 0));
    console.log(resultList);
    return resultList;
}