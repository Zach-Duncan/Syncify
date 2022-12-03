import React from "react";
import { Redirect, Route, Switch } from "react-router-dom";
import { LandingPage } from "../pages/landing-page/landing-page";
import { NotFoundPage } from "../pages/not-found";
import { useUser } from "../authentication/use-auth";
import { PageWrapper } from "../components/page-wrapper/page-wrapper";

import { ShoppingListListingPage } from "../pages/shopping-lists/listing-page/shopping-list-listing";
import { ShoppingListCreatePage } from "../pages/shopping-lists/create-page/shopping-list-create";
import { ShoppingListUpdatePage } from "../pages/shopping-lists/update-page/shopping-list-update";
import { ShoppingListDeletePage } from "../pages/shopping-lists/delete-page/shopping-list-delete";
import { UnitListingPage } from "../pages/units/listing-page/units-listing";
import { UnitCreatePage } from "../pages/units/create-page/unit-create";
import { UnitUpdatePage } from "../pages/units/update-page/unit-update";
import { UnitDeletePage } from "../pages/units/delete-page/unit-delete";
import { ToDoListingPage } from "../pages/to-dos/listing-page/listing page/to-dos-listing";
import { ToDoCreatePage } from "../pages/to-dos/listing-page/create page/to-dos-create";
import { ToDoUpdatePage } from "../pages/to-dos/listing-page/update page/todos-update";
import { ToDoDeletePage } from "../pages/to-dos/listing-page/delete-page/to-dos-delete";
import { RecipeIngredientsListingPage } from "../pages/recipe-ingredients-page/listing-page/recipe-ingredient-listing";
import { RecipeIngredientsCreatePage } from "../pages/recipe-ingredients-page/create-page/recipe-ingredient-create";
import { RecipeIngredientsUpdatePage } from "../pages/recipe-ingredients-page/update-page/recipe-ingredient-update";
import { RecipeIngredientsDeletePage } from "../pages/recipe-ingredients-page/delete-page/recipe-ingredient-delete";
import { GroupListingPage } from "../pages/group-page/listing-page/group-listing";
import { GroupCreatePage } from "../pages/group-page/create-page/group-create";
import { GroupUpdatePage } from "../pages/group-page/update-page/group-update";
import { RecipeListingPage } from "../pages/recipes-page/listing-page/recipe-listing";
import { RecipeCreatePage } from "../pages/recipes-page/create-page/recipe-create";
import { RecipeUpdatePage } from "../pages/recipes-page/update-page/recipe-update";
import { RecipeDeletePage } from "../pages/recipes-page/delete-page/recipe-delete";
import { MealTypeListingPage } from "../pages/meal-types/listing-page/meal-type-listing";
import { IngredientListingPage } from "../pages/ingredients/listing-page/ingredient-listing";
import { IngredientCreatePage } from "../pages/ingredients/create-page/ingredient-create";
import { IngredientUpdatePage } from "../pages/ingredients/update-page/ingredient-update";
import { IngredientDeletePage } from "../pages/ingredients/delete-page/ingredient-delete";
import { UsersListingPage } from "../pages/users-page/listing-page/user-listing";
import { UsersProfilePage } from "../pages/users-page/profile-page/users-profile";
import { UsersUpdatePage } from "../pages/users-page/update-page/user-update";
import { UsersDeletePage } from "../pages/users-page/delete-page/user-delete";
import { MemberRoleListingPage } from "../pages/member-role-page/listing-page/member-role-listing-page";
import { EventListingPage } from "../pages/events-page/listing-page/events-listing";
import { EventCreatePage } from "../pages/events-page/create-page/events-create";
import { EventUpdatePage } from "../pages/events-page/update-page/events-update";
import { EventDeletePage } from "../pages/events-page/delete-page/events-delete";
import { GroupMembersListingPage } from "../pages/Group-Members-page/listing-page/group-members-listing";
import { GroupMembersUpdatePage } from "../pages/Group-Members-page/update-page/group-members-update";
import { GroupMemberCreatePage } from "../pages/Group-Members-page/create-page/group-members-create";
import { GroupMembersDeletePage } from "../pages/Group-Members-page/delete-page/group-members-delete";

import App from "../components/calendar/calendar";
import UserCreateModal from "../pages/users-page/create-page/user-create";
import { LoginPage } from "../pages/login-page/login-page";

//import { ShoppingListUpdatePage } from "../pages/shopping-lists/update-page/shopping-list-update";
//This is where you will declare all of your routes (the ones that show up in the search bar)
export const routes = {
  root: `/`,
  home: `/home`,
  user: "/user",
  register: "/register",
  calendar: "/calendar",
  users: {
    listing: "/users",
    profile: "/users/profile",
    create: "/users/create",
    update: "/users/:id",
    delete: "/users/delete/:id",
  },
  mealTypes: {
    listing: "/meal-types",
  },
  ingredients: {
    listing: "/ingredients",
    create: "/ingredients/create",
    update: "/ingredients/:id",
    delete: "/ingredients/delete/:id",
  },
  memberRoles: {
    listing: "/member-roles",
  },
  recipes: {
    listing: "/recipes",
    create: "/recipes/create",
    update: "/recipes/:id",
    delete: "/recipes/delete/:id",
  },
  shoppingLists: {
    listing: "/shopping-lists",
    create: "/shopping-lists/create",
    update: "/shopping-lists/:id",
    delete: "/shopping-lists/delete/:id",
  },
  groups: {
    listing: "/groups",
    create: "/groups/create",
    update: "/groups/:id",
  },
  toDos: {
    listing: "/to-dos",
    create: "/to-dos/create",
    update: "/to-dos/:id",
    delete: "/to-dos/delete/:id",
  },
  units: {
    listing: "/units",
    create: "/units/create",
    update: "/units/:id",
    delete: "/units/delete/:id",
  },
  events: {
    listing: "/events",
    create: "/events/create",
    update: "/events/:id",
    delete: "/events/delete/:id",
  },
  groupMembers: {
    listing: "/group-members",
    create: "/group-members/create",
    update: "/group-members/:id",
    delete: "/group-members/delete/:id",
  },
  recipeIngredients: {
    listing: "/recipe-ingredients",
    create: "recipe-ingredients/create",
    update: "recipe-ingredients/:id",
    delete: "recipe-ingredients/delete/:id",
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
          {/* Going to route "localhost:5001/" will go to Login Page */}
          <Route path={routes.root} exact>
            <Redirect to={routes.home} />
          </Route>
          {/* When path === / render LandingPage */}
          <Route path={routes.home} exact>
            <LandingPage />
          </Route>
          {/* When path === /iser render UserPage */}
          {/* <Route path={routes.calendar} exact>
            <App />
          </Route> */}
          <Route path={routes.users.create} exact>
            <UserCreateModal />
          </Route>
          <Route path={routes.users.listing} exact>
            <UsersListingPage />
          </Route>
          <Route path={routes.users.profile} exact>
            <UsersProfilePage />
          </Route>
          <Route path={routes.users.update} exact>
            <UsersUpdatePage />
          </Route>
          <Route path={routes.users.delete} exact>
            <UsersDeletePage />
          </Route>
          <Route path={routes.mealTypes.listing} exact>
            <MealTypeListingPage />
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
          <Route path={routes.ingredients.delete} exact>
            <IngredientDeletePage />
          </Route>
          <Route path={routes.recipes.listing} exact>
            <RecipeListingPage />
          </Route>
          <Route path={routes.recipes.create} exact>
            <RecipeCreatePage />
          </Route>
          <Route path={routes.recipes.update} exact>
            <RecipeUpdatePage />
          </Route>
          <Route path={routes.recipes.delete} exact>
            <RecipeDeletePage />
          </Route>
          <Route path={routes.groups.listing} exact>
            <GroupListingPage />
          </Route>
          <Route path={routes.groups.create} exact>
            <GroupCreatePage />
          </Route>
          <Route path={routes.groups.update} exact>
            <GroupUpdatePage />
          </Route>
          <Route path={routes.shoppingLists.create} exact>
            <ShoppingListCreatePage />
          </Route>
          <Route path={routes.shoppingLists.listing} exact>
            <ShoppingListListingPage />
          </Route>
          <Route path={routes.shoppingLists.update} exact>
            <ShoppingListUpdatePage />
          </Route>
          <Route path={routes.units.listing} exact>
            <UnitListingPage />
          </Route>
          <Route path={routes.units.create} exact>
            <UnitCreatePage />
          </Route>
          <Route path={routes.units.update} exact>
            <UnitUpdatePage />
          </Route>
          <Route path={routes.units.delete} exact>
            <UnitDeletePage />
          </Route>
          <Route path={routes.shoppingLists.delete} exact>
            <ShoppingListDeletePage />
          </Route>
          <Route path={routes.toDos.listing} exact>
            <ToDoListingPage />
          </Route>
          <Route path={routes.toDos.create} exact>
            <ToDoCreatePage />
          </Route>
          <Route path={routes.toDos.update} exact>
            <ToDoUpdatePage />
          </Route>
          <Route path={routes.toDos.delete} exact>
            <ToDoDeletePage />
          </Route>
          <Route path={routes.memberRoles.listing} exact>
            <MemberRoleListingPage />
          </Route>
          <Route path={routes.events.listing} exact>
            <EventListingPage />
          </Route>
          <Route path={routes.events.update} exact>
            <EventUpdatePage />
          </Route>
          <Route path={routes.events.create} exact>
            <EventCreatePage />
          </Route>
          <Route path={routes.events.delete} exact>
            <EventDeletePage />
          </Route>
          <Route path={routes.groupMembers.listing} exact>
            <GroupMembersListingPage />
          </Route>
          <Route path={routes.groupMembers.update} exact>
            <GroupMembersUpdatePage />
          </Route>
          <Route path={routes.groupMembers.create} exact>
            <GroupMemberCreatePage />
          </Route>
          <Route path={routes.groupMembers.delete} exact>
            <GroupMembersDeletePage />
          </Route>
          <Route path={routes.recipeIngredients.listing} exact>
            <RecipeIngredientsListingPage />
          </Route>
          <Route path={routes.recipeIngredients.create} exact>
            <RecipeIngredientsCreatePage />
          </Route>
          <Route path={routes.recipeIngredients.update} exact>
            <RecipeIngredientsUpdatePage />
          </Route>
          <Route path={routes.recipeIngredients.delete} exact>
            <RecipeIngredientsDeletePage />
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
