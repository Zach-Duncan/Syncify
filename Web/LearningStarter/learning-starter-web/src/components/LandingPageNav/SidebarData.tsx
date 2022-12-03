import React from "react";
import * as FaIcons from "react-icons/fa";
import * as AiIcons from "react-icons/ai";
import * as IoIcons from "react-icons/io";
import * as GiIcons from "react-icons/gi";
export const SidebarData = [
  {
    title: "Home",
    path: "/",
    icon: <AiIcons.AiFillHome />,
    cName: "nav-text",
  },
  {
    title: "User",
    path: "/users",
    icon: <IoIcons.IoMdPerson />,
    cName: "nav-text",
  },
  {
    title: "Events",
    path: "/events",
    icon: <IoIcons.IoMdPeople />,
    cName: "nav-text",
  },
  {
    title: "Todos",
    path: "/to-dos",
    icon: <FaIcons.FaCartPlus />,
    cName: "nav-text",
  },
  {
    title: "Recipes",
    path: "/recipes",
    icon: <IoIcons.IoMdNutrition />,
    cName: "nav-text",
  },
  {
    title: "Shopping Lists",
    path: "/shopping-lists",
    icon: <IoIcons.IoMdCart />,
    cName: "nav-text",
  },
  {
    title: "Ingredients",
    path: "/ingredients",
    icon: <IoIcons.IoMdNutrition />,
    cName: "nav-text",
  },
  {
    title: "Units",
    path: "/units",
    icon: <GiIcons.GiWeight />,
    cName: "nav-text",
  },
];
