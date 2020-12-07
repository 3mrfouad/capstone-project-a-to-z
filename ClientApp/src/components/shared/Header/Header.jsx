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
import { logout } from "../../../actions/instructorActions";

const Header = () => {
  const dispatch = useDispatch();
  const { user, success, loading } = useSelector(
    (state) => state.userLoginState
  );

  const logoutHandler = () => {
    console.log("logout");
    dispatch(logout());
  };
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Navbar bg="light" expand="lg" collapseOnSelect>
          <Container>
            <LinkContainer to="/">
              <Navbar.Brand>AZ Learn</Navbar.Brand>
            </LinkContainer>
            {user && user.isInstructor ? (
              <React.Fragment>
                <LinkContainer to="/cohortsummary">
                  <Navbar.Brand>Cohorts</Navbar.Brand>
                </LinkContainer>
                {/* <LinkContainer to={`/coursesummary/3`}>
                  <Navbar.Brand>Course Summary</Navbar.Brand>
                </LinkContainer> */}
                <LinkContainer to={`/registeruser`}>
                  <Navbar.Brand>Register Users</Navbar.Brand>
                </LinkContainer>
                <LinkContainer to={`/managecourse`}>
                  <Navbar.Brand>Manage Course</Navbar.Brand>
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
                {user ? (
                  <LinkContainer to="/" onClick={logoutHandler}>
                    <Nav.Link>
                      {" "}
                      <strong>LogOut</strong>{" "}
                    </Nav.Link>
                  </LinkContainer>
                ) : (
                  ""
                )}

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
