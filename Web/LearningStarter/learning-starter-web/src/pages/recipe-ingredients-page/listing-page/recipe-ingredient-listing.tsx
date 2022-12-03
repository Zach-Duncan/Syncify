import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Header, Table } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, RecipeIngredientGetDto } from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
//import "./ingredient-listing.css";

export const RecipeIngredientsListingPage = () => {
  const [recipeIngredients, setRecipeIngredients] =
    useState<RecipeIngredientGetDto[]>();
  const history = useHistory();

  useEffect(() => {
    const fetchRecipeIngredients = async () => {
      const response = await axios.get<ApiResponse<RecipeIngredientGetDto[]>>(
        `${BaseUrl}/api/recipe-ingredients`
      );

      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err.message);
        });
      } else {
        setRecipeIngredients(response.data.data);
      }
    };

    fetchRecipeIngredients();
  }, []);

  return (
    <>
      {recipeIngredients && (
        <>
          <Header>Recipe Ingredients</Header>
          <Button
            positive
            icon="circle plus"
            content="Create"
            labelPosition="left"
            onClick={() => history.push(routes.recipeIngredients.create)}
          />
          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Recipe Name</Table.HeaderCell>
                <Table.HeaderCell>Ingredient Name</Table.HeaderCell>
                <Table.HeaderCell>Quantity</Table.HeaderCell>
                <Table.HeaderCell>Unit Name</Table.HeaderCell>
                <Table.HeaderCell>Edit Recipe Ingredients</Table.HeaderCell>
                <Table.HeaderCell>Delete Recipe Ingredients</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {recipeIngredients.map((recipeIngredients) => {
                return (
                  <Table.Row key={recipeIngredients.id}>
                    <Table.Cell>{recipeIngredients.id}</Table.Cell>
                    <Table.Cell>{recipeIngredients.recipe.name}</Table.Cell>
                    <Table.Cell>{recipeIngredients.ingredient.name}</Table.Cell>
                    <Table.Cell>{recipeIngredients.quantity}</Table.Cell>
                    <Table.Cell>{recipeIngredients.unit.name}</Table.Cell>
                    <Table.Cell>
                      <Button
                        positive
                        type="button"
                        content="Edit Ingredients"
                        icon="pencil"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.recipeIngredients.update.replace(
                              ":id",
                              `${recipeIngredients.id}`
                            )
                          )
                        }
                      />
                    </Table.Cell>
                    <Table.Cell>
                      <Button
                        negative
                        type="button"
                        content="Delete Ingredients"
                        icon="trash"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.recipeIngredients.delete.replace(
                              ":id",
                              `${recipeIngredients.id}`
                            )
                          )
                        }
                      />
                    </Table.Cell>
                  </Table.Row>
                );
              })}
            </Table.Body>
          </Table>
        </>
      )}
    </>
  );
};
