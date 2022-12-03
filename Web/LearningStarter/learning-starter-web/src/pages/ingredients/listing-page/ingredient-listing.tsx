import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Header, Table } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, IngredientGetDto } from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import "./ingredient-listing.css";

export const IngredientListingPage = () => {
  const [ingredients, setIngredients] = useState<IngredientGetDto[]>();
  const history = useHistory();

  useEffect(() => {
    const fetchIngredients = async () => {
      const response = await axios.get<ApiResponse<IngredientGetDto[]>>(
        `${BaseUrl}/api/ingredients`
      );

      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err.message);
        });
      } else {
        setIngredients(response.data.data);
      }
    };

    fetchIngredients();
  }, []);

  return (
    <>
      {ingredients && (
        <>
          <Header>Ingredients</Header>
          <Button
            positive
            icon="circle plus"
            content="Create"
            labelPosition="left"
            onClick={() => history.push(routes.ingredients.create)}
          />
          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Name</Table.HeaderCell>
                <Table.HeaderCell>Image</Table.HeaderCell>
                <Table.HeaderCell>Delete Ingredients</Table.HeaderCell>
                <Table.HeaderCell>Edit Ingredients</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {ingredients.map((ingredient) => {
                return (
                  <Table.Row key={ingredient.id}>
                    <Table.Cell>{ingredient.id}</Table.Cell>
                    <Table.Cell>{ingredient.name}</Table.Cell>
                    <Table.Cell>{ingredient.image}</Table.Cell>
                    <Table.Cell>
                      <Button
                        positive
                        type="button"
                        content="Edit Ingredient"
                        icon="pencil"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.ingredients.update.replace(
                              ":id",
                              `${ingredient.id}`
                            )
                          )
                        }
                      />
                    </Table.Cell>
                    <Table.Cell>
                      <Button
                        negative
                        type="button"
                        content="Delete Ingredient"
                        icon="trash"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.ingredients.delete.replace(
                              ":id",
                              `${ingredient.id}`
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
