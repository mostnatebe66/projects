using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Reponses;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models;

namespace FlooringMastery.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public LookUpAllOrdersResponse LookUpOrdersByDate(string orderDate)
        {
            LookUpAllOrdersResponse response = new LookUpAllOrdersResponse();

            response.Order = _orderRepository.LoadAllOrders(orderDate);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "Mistakes were made. Contact IT.";
            }
            else if (response.Order.Count == 0)
            {
                response.Success = false;
                response.Message = $"{orderDate} currently contains no orders.";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public OrderAddReponse CreateNewOrder(Order orderToAdd)
        {
            OrderAddReponse addResponse = new OrderAddReponse();
            OrderValidateResponse validateResponse = new OrderValidateResponse();

            validateResponse = ValidateOrder(orderToAdd); //added validation from ValidateOrder method

            if (validateResponse.Success == false)
            {
                addResponse.Success = validateResponse.Success;
                addResponse.Message = validateResponse.Message;
                return addResponse;
            }

            addResponse.Order = _orderRepository.AddOrder(orderToAdd);

            if (addResponse.Order == null)
            {
                addResponse.Success = false;
                addResponse.Message = $"Mistakes were made - Please Contact IT";
            }
            else if (addResponse.Order.CustomerName == string.Empty || addResponse.Order.State == string.Empty || addResponse.Order.Date == string.Empty)
            {
                addResponse.Success = false;
                addResponse.Message = $"Name, State, or Date cannot be empty";
            }
            else if (addResponse.Order.ProductName != "Tile" || addResponse.Order.ProductName != "Wood" || addResponse.Order.ProductName != "Laminate" || addResponse.Order.ProductName != "Carpet" || addResponse.Order.ProductName != "Premium Heated Tile")
            {
                addResponse.Success = false;
                addResponse.Message = $"Product Name must be, Wood, Tile, Laminate, Carpet, or Premium Heated Tile";
            }
            else if (addResponse.Order.Area <= 0)
            {
                addResponse.Success = false;
                addResponse.Message = $"Area must be positive";
            }
            else
            {
                addResponse.Success = true;
            }
            return addResponse;
        }

        public OrderDeleteResponse RemoveOrder(string date, string orderID)
        {
            OrderDeleteResponse response = new OrderDeleteResponse();
            response.DeleteSuccess = _orderRepository.DeleteOrder(date, orderID);

            if (response.DeleteSuccess == true)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = $"Order probably not found or something else was wrong. Error log maybe created? Contact IT";
            }
            return response;
        }

        public OrderEditResponse EditOrder(Order newOrder, Order oldOrder, string oldDate)
        {
            OrderEditResponse editResponse = new OrderEditResponse();
            OrderValidateResponse validateResponse = new OrderValidateResponse();

            validateResponse = ValidateOrder(newOrder); //added validation from ValidateOrder method

            if (validateResponse.Success == false)
            {
                editResponse.Success = validateResponse.Success;
                editResponse.Message = validateResponse.Message;
                return editResponse;
            }
            editResponse.Order = _orderRepository.ChangeOrderContents(newOrder, oldOrder, oldDate);

            if (editResponse.Order == null)
            {
                editResponse.Success = false;
                editResponse.Message = $"Mistakes were made - Please Contact IT";
            }
            else if (editResponse.Order.CustomerName == string.Empty || editResponse.Order.State == string.Empty || editResponse.Order.Date == string.Empty)
            {
                editResponse.Success = false;
                editResponse.Message = $"Name, State, or Date cannot be empty";
            }
            else if (editResponse.Order.ProductName != "Tile" || editResponse.Order.ProductName != "Wood" || editResponse.Order.ProductName != "Laminate" || editResponse.Order.ProductName != "Carpet" || editResponse.Order.ProductName != "Premium Heated Tile")
            {
                editResponse.Success = false;
                editResponse.Message = $"Product Name must be, Wood, Tile, Laminate, Carpet, or Premium Heated Tile";
            }
            else if (editResponse.Order.Area <= 0)
            {
                editResponse.Success = false;
                editResponse.Message = $"Area must be positive.";
            }
            else
            {
                editResponse.Success = true;
            }
            return editResponse;
        }

        public OrderValidateResponse ValidateOrder(Order orderToValidate) //method used for more validation
        {
            OrderValidateResponse response = new OrderValidateResponse();

            if (orderToValidate == null)
            {
                response.Success = false;
                response.Message = $"Mistakes were made - Please Contact IT";
            }
            else if (orderToValidate.CustomerName == string.Empty || orderToValidate.State == string.Empty || orderToValidate.Date == string.Empty)
            {
                response.Success = false;
                response.Message = $"Name, State, or Date cannot be empty";
            }
            else if (orderToValidate.ProductName != "Tile" || orderToValidate.ProductName != "Wood" || orderToValidate.ProductName != "Laminate" || orderToValidate.ProductName != "Carpet" || orderToValidate.ProductName != "Premium Heated Tile")
            {
                response.Success = false;
                response.Message = $"Product Name must be, Wood, Tile, Laminate, Carpet, or Premium Heated Tile";
            }
            else if (orderToValidate.Area <= 0)
            {
                response.Success = false;
                response.Message = $"Area must be positive number.";
            }
            else
            {
                response.Success = true;
            }
            return response;

        }

        public SingleOrderLookUpResponse LookUpSingleOrder(string orderDate, string orderID)
        {
            SingleOrderLookUpResponse response = new SingleOrderLookUpResponse();

            response.order = _orderRepository.LoadOrder(orderDate, orderID);

            if (response.order == null)
            {
                response.Success = false;
                response.Message = $"Mistakes were made. Contact IT";
            }
            if (response.order != null)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = $"Order not found";
            }
            return response;
        }
    }
}

