import React, { useState } from "react";
import {
  Navbar,
  Nav,
  NavDropdown,
  Form,
  FormControl,
  Button,
  Container,
} from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import Loader from "../Loader/Loader";

const Header = () => {
  const { user, success, loading } = useSelector(
    (state) => state.userLoginState
  );
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Navbar bg="light" expand="lg" collapseOnSelect>
          <Container>
            <LinkContainer to="/">
              <Navbar.Brand>XXXXX SYSTEM</Navbar.Brand>
            </LinkContainer>
            {user && user.isInstructor ? (
              <React.Fragment>
                <LinkContainer to="/cohortsummary">
                  <Navbar.Brand>Cohorts</Navbar.Brand>
                </LinkContainer>
                <LinkContainer to={`/coursesummary/3`}>
                  <Navbar.Brand>Courses</Navbar.Brand>
                </LinkContainer>
              </React.Fragment>
            ) : (
              ""
            )}
            {user && !user.isInstructor ? (
              <React.Fragment>
                <LinkContainer to={`/student/${user.userId}`}>
                  <Navbar.Brand>Courses</Navbar.Brand>
                </LinkContainer>
              </React.Fragment>
            ) : (
              ""
            )}
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
              <Nav className="ml-auto">
                <LinkContainer to="/">
                  <Nav.Link>
                    {" "}
                    <i className="fas fa-shopping-cart"></i>{" "}
                    <strong>Hello {user ? user.name : ""}</strong>{" "}
                  </Nav.Link>
                </LinkContainer>
                {/* {userInfo ? (
                <NavDropdown title={userInfo.name} id="username">
                  <LinkContainer to="/profile">
                    <NavDropdown.Item>Profile</NavDropdown.Item>
                  </LinkContainer>
                  <NavDropdown.Item onClick={logoutHandler}>
                    Logout
                  </NavDropdown.Item>
                </NavDropdown>
              ) : (
                <LinkContainer to="/login">
                  <Nav.Link>
                    {" "}
                    <i className="fas fa-user"></i> <strong>Sign In</strong>{" "}
                  </Nav.Link>
                </LinkContainer>
              )} */}
                {/* {
                userInfo && userInfo.isAdmin && (
                  <NavDropdown title='Admin' id="adminmenu">
                  <LinkContainer to="/admin/userlist">
                    <NavDropdown.Item>Users</NavDropdown.Item>
                  </LinkContainer>
                  <LinkContainer to="/admin/productlist">
                    <NavDropdown.Item>Products</NavDropdown.Item>
                  </LinkContainer>
                  <LinkContainer to="/admin/orderlist">
                    <NavDropdown.Item>Orders</NavDropdown.Item>
                  </LinkContainer>
                </NavDropdown>
                )
              } */}
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>
      )}
    </React.Fragment>
  );
};

export default Header;
