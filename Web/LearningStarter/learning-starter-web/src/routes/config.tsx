import React from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import { LandingPage } from "../pages/landing-page/landing-page";
import { NotFoundPage } from "../pages/not-found";
import { useUser } from "../authentication/use-auth";
import { UserPage } from "../pages/user-page/user-page";
import { PageWrapper } from "../components/page-wrapper/page-wrapper";
import { MealTypeListingPage } from "../pages/meal-types/listing-page/meal-type-listing";
import { MealTypeCreatePage } from "../pages/meal-types/create-page/meal-type-create";
import { MealTypeUpdatePage } from "../pages/meal-types/update-page/meal-type-update";
import { IngredientListingPage } from "../pages/ingredients/listing-page/ingredient-listing";
import { IngredientCreatePage } from "../pages/ingredients/create-page/ingredient-create";
import { IngredientUpdatePage } from "../pages/ingredients/update-page/ingredient-update";
import { ShoppingListListingPage } from "../pages/shopping-lists/listing-page/shopping-lists-listing";
import { ShoppingListCreatePage } from "../pages/shopping-lists/create-page/shopping-lists-create";
import { ShoppingListUpdatePage } from "../pages/shopping-lists/update-page/shopping-list-update";
//import { ShoppingListUpdatePage } from "../pages/shopping-lists/update-page/shopping-list-update";
//This is where you will declare all of your routes (the ones that show up in the search bar)
export const routes = {
  root: `/`,
  home: `/home`,
  user: `/user`,
  mealTypes: {
    listing: '/meal-types',
    create: "/meal-types/create",
    update: "/meal-types/:id",
  },
  ingredients: {
    listing: '/ingredients',
    create: "/ingredients/create",
    update: "/ingredients/:id",
  },
  shoppingList: {
    listing: '/shopping-lists',
    create: "/shopping-lists/create",
    update: "/shopping-lists/:id",
  },
};

//This is where you will tell React Router what to render when the path matches the route specified.
export const Routes = () => {
  //Calling the useUser() from the use-auth.tsx in order to get user information
  const user = useUser();
  return (
    <>
      {/* The page wrapper is what shows the NavBar at the top, it is around all pages inside of here. */}
      <PageWrapper user={user}>
        <Switch>
          {/* When path === / render LandingPage */}
          <Route path={routes.home} exact>
            <LandingPage />
          </Route>
          {/* When path === /iser render UserPage */}
          <Route path={routes.user} exact>
            <UserPage />
          </Route>
          {/* Going to route "localhost:5001/" will go to homepage */}
          <Route path={routes.root} exact>
            <Redirect to={routes.home} />
          </Route>
          <Route path={routes.mealTypes.listing} exact>
            <MealTypeListingPage />
          </Route>
          <Route path={routes.mealTypes.create} exact>
            <MealTypeCreatePage />
          </Route>
          <Route path={routes.mealTypes.update} exact>
            <MealTypeUpdatePage />
          </Route>
          <Route path={routes.ingredients.listing} exact>
            <IngredientListingPage />
          </Route>
          <Route path={routes.ingredients.create} exact>
            <IngredientCreatePage />
          </Route>
          <Route path={routes.ingredients.update} exact>
            <IngredientUpdatePage />
          </Route>     
          <Route path={routes.shoppingList.create} exact>
            <ShoppingListCreatePage />
          </Route>
          <Route path={routes.shoppingList.listing} exact>
            <ShoppingListListingPage />
          </Route>
          <Route path={routes.shoppingList.update} exact>
            <ShoppingListUpdatePage />
          </Route>
          {/* This should always come last.  
            If the path has no match, show page not found */}
          <Route path="*" exact>
            <NotFoundPage />
          </Route>
        </Switch>
      </PageWrapper>
    </>
  );
};
