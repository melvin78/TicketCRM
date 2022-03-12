﻿using System.Reflection;

namespace TicketCRM.DomainLayer.MainBoundedContext.ValueObjects
{
    /// <summary>
    /// Base class for value objects in domain.
    /// </summary>
    /// <typeparam name="TValueObject">The type of this value object</typeparam>
    public class ValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : ValueObject<TValueObject>
    {
        #region IEquatable and Override Equals operators

        /// <summary>
        /// <see cref="M:System.Object.IEquatable{TValueObject}"/>
        /// </summary>
        /// <param name="other"><see cref="M:System.Object.IEquatable{TValueObject}"/></param>
        /// <returns><see cref="M:System.Object.IEquatable{TValueObject}"/></returns>
        public bool Equals(TValueObject other)
        {
            if ((object)other == null)
                return false;

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            //compare all public properties
            PropertyInfo[] publicProperties = GetType().GetProperties();

            if (publicProperties != null
                &&
                publicProperties.Any())
            {
                return publicProperties.All(p =>
                {
                    var left = p.GetValue(this, null);
                    var right = p.GetValue(other, null);


                    if (typeof(TValueObject).IsAssignableFrom(left.GetType()))
                    {
                        //check not self-references...
                        return ReferenceEquals(left, right);
                    }
                    else
                        return left.Equals(right);
                });
            }
            else
                return true;
        }
        /// <summary>
        /// <see cref="M:System.Object.Equals"/>
        /// </summary>
        /// <param name="obj"><see cref="M:System.Object.Equals"/></param>
        /// <returns><see cref="M:System.Object.Equals"/></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
                return true;
            
            if (obj is ValueObject<TValueObject> item)
                return Equals((TValueObject)item);
            else
                return false;

        }
        /// <summary>
        /// <see cref="M:System.Object.GetHashCode"/>
        /// </summary>
        /// <returns><see cref="M:System.Object.GetHashCode"/></returns>
        public override int GetHashCode()
        {
            int hashCode = 31;
            bool changeMultiplier = false;
            int index = 1;

            //compare all public properties
            PropertyInfo[] publicProperties = this.GetType().GetProperties();
            
            if (publicProperties != null
                &&
                publicProperties.Any())
            {
                foreach (var item in publicProperties)
                {
                    object value = item.GetValue(this, null);

                    if (value != null)
                    {
                        hashCode = hashCode * ((changeMultiplier) ? 59 : 114) + value.GetHashCode();

                        changeMultiplier = !changeMultiplier;
                    }
                    else
                        hashCode = hashCode ^ (index * 13);//only for support {"a",null,null,"a"} <> {null,"a","a",null}
                }
            }

            return hashCode;
        }

        public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            if (Equals(left, null))
                return (Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            return !(left == right);
        }

        #endregion
    }
}
